using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.GapDashboard.Queries.GetDashboardONPE
{
    // Dashboard de madurez estilo ONPE -- replica la hoja "Dashboard" del Excel
    // "FM03-GPP_GC Declaración de aplicabilidad del SGSI (SoA)". Arranca solo con el cuadro
    // superior (Madurez General + tabla de los 4 grupos del Anexo A ISO 27001: Organizacionales/
    // Personas/Físicos/Tecnológicos); la dona y los 5 radares que trae la hoja original quedan
    // para una siguiente iteración.
    //
    // Usa el mismo GapItemsBuilder que GetSoaReportQuery/GetGapDashboardQuery (no una query propia
    // contra ControlEvaluation) para que el nivel de madurez de cada control no diverja entre
    // pantallas.
    public class GetDashboardONPEQuery : IGetDashboardONPEQuery
    {
        // Meta fija en 5 ("Optimizado") para el general y los 4 grupos -- igual que la columna
        // "Meta" del Excel de ONPE, que no varía por grupo.
        private const decimal Meta = 5m;

        private readonly IDatabaseService _databaseService;
        private readonly GapItemsBuilder _itemsBuilder;

        public GetDashboardONPEQuery(IDatabaseService databaseService, GapItemsBuilder itemsBuilder)
        {
            _databaseService = databaseService;
            _itemsBuilder = itemsBuilder;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var currentEvaluation = await (
                    from eval in _databaseService.Evaluation
                    join standard in _databaseService.Standard on eval.standardId equals standard.standardId
                    where (eval.isDeleted == null || eval.isDeleted == false)
                        && eval.companyId == companyId
                        && eval.isCurrent
                    select new { eval.evaluationId, eval.standardId, standardName = standard.name, evaluationDescription = eval.description }
                ).FirstOrDefaultAsync();

                if (currentEvaluation == null)
                    return new GetDashboardONPEDto { hasCurrentEvaluation = false };

                var (controlItems, _) = await _itemsBuilder.BuildControlItems(
                    currentEvaluation.standardId, currentEvaluation.evaluationId, userId: 0, scopeToUser: false);

                // Todos los controles de la evaluación cuentan, incluidos los que todavía no
                // tienen nota (Pendiente) -- esos valen 0, no se excluyen del promedio ni de la
                // lista de grupos. Antes se descartaban del todo (como hace AVERAGE() con celdas
                // vacías en Excel), pero eso podía hacer desaparecer grupos completos -- o inflar
                // el promedio -- mientras la evaluación recién empieza y la mayoría de controles
                // siguen sin evaluar. Los marcados "No aplica" sí se siguen excluyendo: un
                // control que no aplica no debería contar como 0 de madurez (ahí sí se mantiene
                // el mismo criterio que el "-" del Excel).
                var counted = controlItems.Where(i => !i.isNotApplicable).ToList();

                var madurezGeneral = counted.Count > 0 ? counted.Average(i => i.value ?? 0m) : 0m;

                var grupos = counted
                    .GroupBy(i => new { i.groupNumber, i.theme })
                    .OrderBy(g => g.Key.groupNumber ?? 0)
                    .Select(g =>
                    {
                        var actual = g.Average(i => i.value ?? 0m);
                        return new GetDashboardONPEGroupDto
                        {
                            grupo = g.Key.theme,
                            actual = Math.Round(actual, 2),
                            meta = Meta,
                            actualPercent = Math.Round(actual / Meta, 4),
                        };
                    })
                    .ToList();

                // Detalle control por control -- acá sí van TODOS los controles (incluidos
                // "No aplica" y "Pendiente"), no solo los que entran en "counted": esta lista es
                // el inventario completo del Anexo A, no un promedio. Mismo orden que
                // GetSoaReportQuery (por groupNumber y luego código natural) para que el orden no
                // diverja entre pantallas.
                var controles = controlItems
                    .OrderBy(i => i.groupNumber ?? -1m)
                    .ThenBy(i => GapItemsBuilder.NaturalSortKey(i.code))
                    .Select(i => new GetDashboardONPEControlDto
                    {
                        grupo = i.theme,
                        code = i.code,
                        name = i.name,
                        madurez = i.value,
                        madurezColor = i.color,
                        nivel = i.estado,
                        isNotApplicable = i.isNotApplicable,
                    })
                    .ToList();

                return new GetDashboardONPEDto
                {
                    hasCurrentEvaluation = true,
                    evaluationId = currentEvaluation.evaluationId,
                    standardName = currentEvaluation.standardName,
                    evaluationDescription = currentEvaluation.evaluationDescription,
                    madurezGeneral = Math.Round(madurezGeneral, 2),
                    madurezGeneralPercent = Math.Round(madurezGeneral / Meta, 4),
                    grupos = grupos,
                    controles = controles,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
