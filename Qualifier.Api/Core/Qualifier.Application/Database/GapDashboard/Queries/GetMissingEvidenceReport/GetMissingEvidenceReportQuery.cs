using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.GapDashboard.Queries.GetMissingEvidenceReport
{
    // Reporte "Ítems evaluados sin evidencia" para el dueño del requisito: complementa el "%
    // con evidencia" del dashboard de Inicio mostrando cuáles ítems son los que faltan. Usa el
    // mismo GapItemsBuilder que GetGapDashboardQuery para que el conteo de acá no contradiga el
    // que ya ve en Inicio. Company-wide (sin scopeToUser): es un reporte de gestión, no la vista
    // acotada de un ejecutor.
    public class GetMissingEvidenceReportQuery : IGetMissingEvidenceReportQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly GapItemsBuilder _itemsBuilder;

        public GetMissingEvidenceReportQuery(IDatabaseService databaseService, GapItemsBuilder itemsBuilder)
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
                    where (eval.isDeleted == null || eval.isDeleted == false)
                        && eval.companyId == companyId
                        && eval.isCurrent
                    select new { eval.evaluationId, eval.standardId }
                ).FirstOrDefaultAsync();

                if (currentEvaluation == null)
                    return new GetMissingEvidenceReportDto { hasCurrentEvaluation = false };

                var (controlItems, _) = await _itemsBuilder.BuildControlItems(
                    currentEvaluation.standardId, currentEvaluation.evaluationId, userId: 0, scopeToUser: false);
                var (requirementItems, _) = await _itemsBuilder.BuildRequirementItems(
                    currentEvaluation.standardId, currentEvaluation.evaluationId, userId: 0, scopeToUser: false);

                var allItems = controlItems.Concat(requirementItems).ToList();
                var evaluatedItems = allItems
                    .Where(i => i.estado != GapItemsBuilder.PENDIENTE && i.estado != GapItemsBuilder.NO_APLICA)
                    .ToList();

                var missingItems = evaluatedItems
                    .Where(i => !i.hasEvidence)
                    .OrderBy(i => i.theme).ThenBy(i => i.code)
                    .Select(i => new GetMissingEvidenceItemDto
                    {
                        tipo = i.tipo,
                        itemId = i.itemId,
                        code = i.code,
                        name = i.name,
                        theme = i.theme,
                        estado = i.estado,
                    })
                    .ToList();

                return new GetMissingEvidenceReportDto
                {
                    hasCurrentEvaluation = true,
                    evaluationId = currentEvaluation.evaluationId,
                    evaluatedItemsCount = evaluatedItems.Count,
                    missingEvidenceCount = missingItems.Count,
                    items = missingItems,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
