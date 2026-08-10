using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.GapDashboard;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Gap.Queries.GetGapSummary
{
    // Resumen de "Análisis del GAP" (gap-home): header %, mapa de brecha por tema y leyenda —
    // todo lo que antes calculaba gap-home.component.ts con getters sobre el arreglo completo
    // de ítems (stats/temaRows/countEstado/temas). Se separa de GetGapItemsQuery (la lista
    // paginada) porque estos números son GLOBALES: hoy ya se calculaban desde this.items (no
    // desde visibleItems), o sea que no cambian con la búsqueda/filtro de tema ni con qué
    // página está cargada — este endpoint solo mueve ese cálculo al servidor.
    //
    // Reutiliza GapItemsBuilder (compartido con GetGapDashboardQuery) en vez de reconstruir el
    // árbol completo de GetRequirementEvaluationByProcessQuery/GetControlEvaluationByProcessQuery.
    public class GetGapSummaryQuery : IGetGapSummaryQuery
    {
        private const string PENDIENTE = GapItemsBuilder.PENDIENTE;
        private const string NO_APLICA = GapItemsBuilder.NO_APLICA;
        private const string CUMPLE = GapItemsBuilder.CUMPLE;
        private const string THEME_REQUISITOS = "Cláusulas";
        private const string PENDIENTE_COLOR = "#EAEDEF";

        private readonly IGetCurrentEvaluationQuery _getCurrentEvaluationQuery;
        private readonly IGetAllMaturityLevelsByCompanyIdQuery _getAllMaturityLevelsByCompanyIdQuery;
        private readonly IGetAllResponsiblesByStandardIdQuery _getAllResponsiblesByStandardIdQuery;
        private readonly GapItemsBuilder _itemsBuilder;

        public GetGapSummaryQuery(
            IGetCurrentEvaluationQuery getCurrentEvaluationQuery,
            IGetAllMaturityLevelsByCompanyIdQuery getAllMaturityLevelsByCompanyIdQuery,
            IGetAllResponsiblesByStandardIdQuery getAllResponsiblesByStandardIdQuery,
            GapItemsBuilder itemsBuilder)
        {
            _getCurrentEvaluationQuery = getCurrentEvaluationQuery;
            _getAllMaturityLevelsByCompanyIdQuery = getAllMaturityLevelsByCompanyIdQuery;
            _getAllResponsiblesByStandardIdQuery = getAllResponsiblesByStandardIdQuery;
            _itemsBuilder = itemsBuilder;
        }

        public async Task<Object> Execute(int companyId, int userId, bool scopeToUser)
        {
            try
            {
                // Mismo patrón secuencial que GetEvaluacionBootstrapQuery/GetGapDashboardQuery:
                // IDatabaseService es Scoped (un solo DbContext por request), no soporta
                // llamadas concurrentes.
                var evalResult = await _getCurrentEvaluationQuery.Execute(0);
                if (evalResult is BaseErrorResponseDto) return evalResult;
                if (evalResult is not GetCurrentEvaluationDto evaluation) return BaseApplication.getExceptionErrorResponse();

                var maturityResult = await _getAllMaturityLevelsByCompanyIdQuery.Execute(companyId);
                if (maturityResult is BaseErrorResponseDto) return maturityResult;
                var maturityLevels = ((BaseResponseDto<GetAllMaturityLevelsByCompanyIdDto>)maturityResult).data;

                var responsiblesResult = await _getAllResponsiblesByStandardIdQuery.Execute(evaluation.standardId);
                if (responsiblesResult is BaseErrorResponseDto) return responsiblesResult;
                var responsibles = ((BaseResponseDto<GetAllResponsiblesByStandardIdDto>)responsiblesResult).data;

                var (controlItems, _) = await _itemsBuilder.BuildControlItems(evaluation.standardId, evaluation.evaluationId, userId, scopeToUser);
                var (requirementItems, _) = await _itemsBuilder.BuildRequirementItems(evaluation.standardId, evaluation.evaluationId, userId, scopeToUser);
                var allItems = requirementItems.Concat(controlItems).ToList();

                var evaluatedItems = allItems.Where(i => i.estado != PENDIENTE && i.estado != NO_APLICA).ToList();
                var compliantItems = evaluatedItems.Where(i => i.estado == CUMPLE).ToList();
                var pct = evaluatedItems.Count > 0 ? (int)Math.Round(compliantItems.Count * 100.0 / evaluatedItems.Count) : 0;

                // Orden de los grupos de control por su número real (2, 3, 4... 6.1, 6.2, 7...),
                // ya expuesto por GapItemsBuilder como groupNumber (decimal) — antes se
                // reparseaba el prefijo de "code" como int, lo que hacía que dos sub-grupos con
                // el mismo número entero (ej. "6.1" y "6.2") empataran en el orden. Sin esto,
                // ordenar los temas alfabéticamente pondría "Controles de personas" antes que
                // "Controles Organizacionales", distinto al orden que ya mostraba la pantalla.
                var controlGroupOrder = controlItems
                    .GroupBy(i => i.theme)
                    .Select(g => new { theme = g.Key, order = g.Min(i => i.groupNumber ?? 0) })
                    .OrderBy(x => x.order)
                    .Select(x => x.theme);

                var temas = new List<string> { "Todos" };
                if (requirementItems.Count > 0) temas.Add(THEME_REQUISITOS);
                temas.AddRange(controlGroupOrder);

                var nivelesOrdenados = maturityLevels.OrderByDescending(m => m.value ?? 0).ToList();

                var temaRows = temas.Where(t => t != "Todos").Select(theme =>
                {
                    var items = allItems.Where(i => i.theme == theme).ToList();
                    var segments = nivelesOrdenados.Select(ml => new GetGapSummarySegmentDto
                    {
                        name = ml.name,
                        color = ml.color,
                        count = items.Count(i => i.estado == ml.name),
                    }).ToList();
                    var pendiente = items.Count(i => i.estado == PENDIENTE);
                    return new GetGapSummaryThemeRowDto
                    {
                        theme = theme,
                        total = items.Count,
                        evaluados = items.Count - pendiente,
                        pendiente = pendiente,
                        segments = segments,
                    };
                }).ToList();

                var legend = nivelesOrdenados.Select(ml => new GetGapSummarySegmentDto
                {
                    name = ml.name,
                    color = ml.color,
                    count = allItems.Count(i => i.estado == ml.name),
                }).ToList();
                legend.Add(new GetGapSummarySegmentDto { name = PENDIENTE, color = PENDIENTE_COLOR, count = allItems.Count(i => i.estado == PENDIENTE) });

                return new GetGapSummaryDto
                {
                    evaluation = evaluation,
                    maturityLevels = maturityLevels,
                    responsibles = responsibles,
                    temas = temas,
                    stats = new GetGapSummaryStatsDto { pct = pct, evaluados = evaluatedItems.Count, total = allItems.Count },
                    temaRows = temaRows,
                    legend = legend,
                    evidencias = allItems.Count(i => i.hasEvidence),
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
