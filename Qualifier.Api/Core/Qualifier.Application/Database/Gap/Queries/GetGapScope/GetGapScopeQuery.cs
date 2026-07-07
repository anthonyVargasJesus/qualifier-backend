using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Gap.Queries.GetGapScope
{
    // Reusa las mismas dos queries que ya arman el árbol completo de
    // requisitos/controles (con su filtrado por familia/grupo asignado al
    // usuario vía scopeToUser) para garantizar el mismo alcance exacto que
    // ve el usuario en Evaluación — solo que acá se descarta todo lo pesado
    // (nombre, descripción, madurez, responsable, evidencias, legend) y se
    // devuelve nada más que {tipo, itemId} por cada ítem evaluado. Así
    // "Plan de acción" no tiene que pedir el árbol completo solo para saber
    // qué brechas le corresponde mostrar.
    public class GetGapScopeQuery : IGetGapScopeQuery
    {
        private readonly IGetRequirementEvaluationByProcessQuery _getRequirementEvaluationByProcessQuery;
        private readonly IGetControlEvaluationByProcessQuery _getControlEvaluationByProcessQuery;

        public GetGapScopeQuery(
            IGetRequirementEvaluationByProcessQuery getRequirementEvaluationByProcessQuery,
            IGetControlEvaluationByProcessQuery getControlEvaluationByProcessQuery)
        {
            _getRequirementEvaluationByProcessQuery = getRequirementEvaluationByProcessQuery;
            _getControlEvaluationByProcessQuery = getControlEvaluationByProcessQuery;
        }

        public async Task<Object> Execute(int standardId, int evaluationId, int userId, bool scopeToUser = false)
        {
            try
            {
                var reqResult = await _getRequirementEvaluationByProcessQuery.Execute(standardId, evaluationId, string.Empty, userId, scopeToUser);
                if (reqResult is BaseErrorResponseDto) return reqResult;

                var ctrlResult = await _getControlEvaluationByProcessQuery.Execute(standardId, evaluationId, userId, scopeToUser);
                if (ctrlResult is BaseErrorResponseDto) return ctrlResult;

                var items = new List<GetGapScopeItemDto>();

                if (reqResult is GetRequirementEvaluationsByProcessResponseDto reqDto && reqDto.requirements != null)
                    CollectRequirementItems(reqDto.requirements, items);

                if (ctrlResult is GetControlEvaluationsByProcessResponseDto ctrlDto && ctrlDto.groups != null)
                    CollectControlItems(ctrlDto.groups, items);

                var response = new BaseResponseDto<GetGapScopeItemDto>();
                response.data = items;
                return response;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private static void CollectRequirementItems(List<GetRequirementEvaluationsByProcessRequirementDto> nodes, List<GetGapScopeItemDto> items)
        {
            foreach (var node in nodes)
            {
                if (node.requirementEvaluations != null)
                {
                    foreach (var evaluation in node.requirementEvaluations)
                    {
                        if (evaluation.requirement != null)
                            items.Add(new GetGapScopeItemDto { tipo = "requisito", itemId = evaluation.requirement.requirementId });
                    }
                }
                if (node.children != null && node.children.Count > 0)
                    CollectRequirementItems(node.children, items);
            }
        }

        private static void CollectControlItems(List<GetControlEvaluationsByProcessControlGroupDto> groups, List<GetGapScopeItemDto> items)
        {
            foreach (var group in groups)
            {
                if (group.controls == null) continue;
                foreach (var control in group.controls)
                {
                    var evaluationCount = control.controlEvaluations?.Count ?? 0;
                    for (var i = 0; i < evaluationCount; i++)
                        items.Add(new GetGapScopeItemDto { tipo = "control", itemId = control.controlId });
                }
            }
        }
    }
}
