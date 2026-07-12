using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.GapDashboard.Queries.GetSoaReport
{
    // Declaración de Aplicabilidad (SOA) — ISO 27001 cláusula 6.1.3 d): endpoint propio, no
    // reutiliza /api/gap/evaluacion (ese trae también los requisitos/cláusulas, que el SOA no
    // necesita, más justificación/evidencia por ítem que tampoco usa). Usa el mismo
    // GapItemsBuilder que el dashboard de Inicio y el reporte de evidencia faltante para que el
    // estado de cada control no diverja entre pantallas.
    public class GetSoaReportQuery : IGetSoaReportQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly GapItemsBuilder _itemsBuilder;

        public GetSoaReportQuery(IDatabaseService databaseService, GapItemsBuilder itemsBuilder)
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
                    select new { eval.evaluationId, eval.standardId, standardName = standard.name }
                ).FirstOrDefaultAsync();

                if (currentEvaluation == null)
                    return new GetSoaReportDto { hasCurrentEvaluation = false };

                // El SOA es solo de controles del Anexo A — las cláusulas (4-10) son requisitos
                // obligatorios de gestión del SGSI, no controles con opción de "aplica/no
                // aplica", así que no se piden acá (ni siquiera se llama BuildRequirementItems).
                var (controlItems, _) = await _itemsBuilder.BuildControlItems(
                    currentEvaluation.standardId, currentEvaluation.evaluationId, userId: 0, scopeToUser: false);

                var controls = controlItems
                    .OrderBy(i => i.theme).ThenBy(i => i.code)
                    .Select(i => new GetSoaReportItemDto
                    {
                        code = i.code,
                        name = i.name,
                        theme = i.theme,
                        aplica = i.estado == GapItemsBuilder.PENDIENTE ? (bool?)null : i.estado != GapItemsBuilder.NO_APLICA,
                        implementacion = i.estado == GapItemsBuilder.PENDIENTE ? "Sin evaluar" : i.estado,
                        justificacion = string.IsNullOrWhiteSpace(i.justification) ? "—" : i.justification!.Trim(),
                    })
                    .ToList();

                return new GetSoaReportDto
                {
                    hasCurrentEvaluation = true,
                    evaluationId = currentEvaluation.evaluationId,
                    standardName = currentEvaluation.standardName,
                    controls = controls,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
