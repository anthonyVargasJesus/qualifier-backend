using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.GapDashboard;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Gap.Queries.GetGapItems
{
    // Lista paginada de ítems evaluables (requisitos + controles) de la evaluación actual,
    // para gap-home. Reemplaza el árbol completo que devolvía /api/gap/evaluacion: acá se
    // pagina, se busca y se filtra por tema server-side (mismos criterios que antes
    // implementaba el getter visibleItems() en gap-home.component.ts), y solo viaja al
    // navegador el DTO plano de la página pedida — no el árbol anidado con
    // referenceDocumentations por ítem.
    //
    // GapItemsBuilder ya trae los ~180 ítems de la evaluación a memoria (tabla chica, joins
    // indexados — barato) antes de filtrar/paginar acá; lo que sí queda paginado de verdad es
    // el payload de red y todo el trabajo de renderizado del cliente. Ver GetGapSummaryQuery
    // para los números globales (%, mapa por tema), que no dependen de esta paginación.
    public class GetGapItemsQuery : IGetGapItemsQuery
    {
        private const string TODOS = "Todos";

        private readonly IGetCurrentEvaluationQuery _getCurrentEvaluationQuery;
        private readonly GapItemsBuilder _itemsBuilder;

        public GetGapItemsQuery(IGetCurrentEvaluationQuery getCurrentEvaluationQuery, GapItemsBuilder itemsBuilder)
        {
            _getCurrentEvaluationQuery = getCurrentEvaluationQuery;
            _itemsBuilder = itemsBuilder;
        }

        public async Task<Object> Execute(int userId, bool scopeToUser, int skip, int pageSize, string search, string theme)
        {
            try
            {
                var evalResult = await _getCurrentEvaluationQuery.Execute(0);
                if (evalResult is BaseErrorResponseDto) return evalResult;
                if (evalResult is not GetCurrentEvaluationDto evaluation) return BaseApplication.getExceptionErrorResponse();

                var (controlItems, _) = await _itemsBuilder.BuildControlItems(evaluation.standardId, evaluation.evaluationId, userId, scopeToUser);
                var (requirementItems, _) = await _itemsBuilder.BuildRequirementItems(evaluation.standardId, evaluation.evaluationId, userId, scopeToUser);

                // Orden real: requisitos ("Cláusulas", groupNumber=null) primero, luego
                // controles agrupados por el número real de su grupo (2, 3, 4... no
                // alfabético). Antes ordenaba por i.theme (string) y "Controles
                // Tecnológicos" quedaba antes que "Controles de personas" porque 'T'
                // (mayúscula) ordena antes que 'd' en ASCII — mismo tipo de problema que
                // ya se corrigió en GetGapSummaryQuery para el orden de los chips de tema.
                IEnumerable<GapItemsBuilder.ItemState> allItems = requirementItems.Concat(controlItems)
                    .OrderBy(i => i.groupNumber ?? -1m).ThenBy(i => GapItemsBuilder.NaturalSortKey(i.code));

                if (!string.IsNullOrWhiteSpace(theme) && theme != TODOS)
                    allItems = allItems.Where(i => i.theme == theme);

                if (!string.IsNullOrWhiteSpace(search))
                {
                    var q = search.Trim();
                    allItems = allItems.Where(i =>
                        i.code.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                        i.name.Contains(q, StringComparison.OrdinalIgnoreCase));
                }

                var filtered = allItems.ToList();
                var page = filtered.Skip(skip).Take(pageSize).Select(i => new GetGapItemsDto
                {
                    tipo = i.tipo,
                    itemId = i.itemId,
                    evaluationItemId = i.evaluationItemId,
                    code = i.code,
                    name = i.name,
                    theme = i.theme,
                    maturityLevelId = i.maturityLevelId,
                    value = i.value,
                    justification = i.justification,
                    improvementActions = i.improvementActions,
                    responsibleId = i.responsibleId,
                    hasEvidence = i.hasEvidence,
                }).ToList();

                var baseResponseDto = new BaseResponseDto<GetGapItemsDto>();
                baseResponseDto.data = page;
                baseResponseDto.pagination = Pagination.GetPagination(filtered.Count, pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
