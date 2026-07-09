using Qualifier.Application.Database.ActionPlanPriority.Queries.GetAllActionPlanPrioritiesByCompanyId;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetAllActionPlanStatussByCompanyId;
using Qualifier.Application.Database.Breach.Queries.GetBreachesScope;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess;
using Qualifier.Application.Database.User.Queries.GetAllUsersByCompanyId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Gap.Queries.GetPlanDeAccionBootstrap
{
    public class GetPlanDeAccionBootstrapQuery : IGetPlanDeAccionBootstrapQuery
    {
        private readonly IGetCurrentEvaluationQuery _getCurrentEvaluationQuery;
        private readonly IGetAllMaturityLevelsByCompanyIdQuery _getAllMaturityLevelsByCompanyIdQuery;
        private readonly IGetRequirementEvaluationByProcessQuery _getRequirementEvaluationByProcessQuery;
        private readonly IGetControlEvaluationByProcessQuery _getControlEvaluationByProcessQuery;
        private readonly IGetBreachesScopeQuery _getBreachesScopeQuery;
        private readonly IGetAllActionPlanStatussByCompanyIdQuery _getAllActionPlanStatussByCompanyIdQuery;
        private readonly IGetAllActionPlanPrioritiesByCompanyIdQuery _getAllActionPlanPrioritiesByCompanyIdQuery;
        private readonly IGetAllUsersByCompanyIdQuery _getAllUsersByCompanyIdQuery;

        public GetPlanDeAccionBootstrapQuery(
            IGetCurrentEvaluationQuery getCurrentEvaluationQuery,
            IGetAllMaturityLevelsByCompanyIdQuery getAllMaturityLevelsByCompanyIdQuery,
            IGetRequirementEvaluationByProcessQuery getRequirementEvaluationByProcessQuery,
            IGetControlEvaluationByProcessQuery getControlEvaluationByProcessQuery,
            IGetBreachesScopeQuery getBreachesScopeQuery,
            IGetAllActionPlanStatussByCompanyIdQuery getAllActionPlanStatussByCompanyIdQuery,
            IGetAllActionPlanPrioritiesByCompanyIdQuery getAllActionPlanPrioritiesByCompanyIdQuery,
            IGetAllUsersByCompanyIdQuery getAllUsersByCompanyIdQuery)
        {
            _getCurrentEvaluationQuery = getCurrentEvaluationQuery;
            _getAllMaturityLevelsByCompanyIdQuery = getAllMaturityLevelsByCompanyIdQuery;
            _getRequirementEvaluationByProcessQuery = getRequirementEvaluationByProcessQuery;
            _getControlEvaluationByProcessQuery = getControlEvaluationByProcessQuery;
            _getBreachesScopeQuery = getBreachesScopeQuery;
            _getAllActionPlanStatussByCompanyIdQuery = getAllActionPlanStatussByCompanyIdQuery;
            _getAllActionPlanPrioritiesByCompanyIdQuery = getAllActionPlanPrioritiesByCompanyIdQuery;
            _getAllUsersByCompanyIdQuery = getAllUsersByCompanyIdQuery;
        }

        public async Task<Object> Execute(int companyId, int userId, int standardId, bool scopeToUser = true)
        {
            try
            {
                // Tanda 1: nada de esto depende de evaluationId, así que arrancan
                // todas juntas.
                var evalTask = _getCurrentEvaluationQuery.Execute(0);
                var maturityTask = _getAllMaturityLevelsByCompanyIdQuery.Execute(companyId);
                var statusesTask = _getAllActionPlanStatussByCompanyIdQuery.Execute(companyId);
                var prioritiesTask = _getAllActionPlanPrioritiesByCompanyIdQuery.Execute(companyId);
                var usersTask = _getAllUsersByCompanyIdQuery.Execute(companyId);
                await Task.WhenAll(evalTask, maturityTask, statusesTask, prioritiesTask, usersTask);

                if (evalTask.Result is BaseErrorResponseDto) return evalTask.Result;
                if (evalTask.Result is not GetCurrentEvaluationDto evaluation) return BaseApplication.getExceptionErrorResponse();
                if (maturityTask.Result is BaseErrorResponseDto) return maturityTask.Result;
                if (statusesTask.Result is BaseErrorResponseDto) return statusesTask.Result;
                if (prioritiesTask.Result is BaseErrorResponseDto) return prioritiesTask.Result;
                if (usersTask.Result is BaseErrorResponseDto) return usersTask.Result;

                // Tanda 2: estas sí necesitan el evaluationId que recién se resolvió.
                var reqTask = _getRequirementEvaluationByProcessQuery.Execute(standardId, evaluation.evaluationId, string.Empty, userId, scopeToUser);
                var ctrlTask = _getControlEvaluationByProcessQuery.Execute(standardId, evaluation.evaluationId, userId, scopeToUser);
                var breachesTask = _getBreachesScopeQuery.Execute(evaluation.evaluationId);
                await Task.WhenAll(reqTask, ctrlTask, breachesTask);

                if (reqTask.Result is BaseErrorResponseDto) return reqTask.Result;
                if (ctrlTask.Result is BaseErrorResponseDto) return ctrlTask.Result;
                if (breachesTask.Result is BaseErrorResponseDto) return breachesTask.Result;

                var items = new List<GetGapScopeItemDto>();
                if (reqTask.Result is GetRequirementEvaluationsByProcessResponseDto reqDto && reqDto.requirements != null)
                    CollectRequirementItems(reqDto.requirements, items);
                if (ctrlTask.Result is GetControlEvaluationsByProcessResponseDto ctrlDto && ctrlDto.groups != null)
                    CollectControlItems(ctrlDto.groups, items);

                var breaches = ((BaseResponseDto<Breach.Queries.GetBreachesScope.GetBreachesScopeDto>)breachesTask.Result).data;
                var maturityLevels = ((BaseResponseDto<GetAllMaturityLevelsByCompanyIdDto>)maturityTask.Result).data;
                var statuses = ((BaseResponseDto<GetAllActionPlanStatussByCompanyIdDto>)statusesTask.Result).data;
                var priorities = ((BaseResponseDto<GetAllActionPlanPrioritiesByCompanyIdDto>)prioritiesTask.Result).data;
                var users = ((BaseResponseDto<GetAllUsersByCompanyIdDto>)usersTask.Result).data;

                var response = new GetPlanDeAccionBootstrapDto
                {
                    evaluation = evaluation,
                    items = items,
                    maturityLevels = maturityLevels,
                    breaches = breaches,
                    actionPlanStatuses = statuses,
                    actionPlanPriorities = priorities,
                    users = users,
                };
                return response;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        // Movidos tal cual desde el extinto GetGapScopeQuery.
        private static void CollectRequirementItems(List<GetRequirementEvaluationsByProcessRequirementDto> nodes, List<GetGapScopeItemDto> items)
        {
            foreach (var node in nodes)
            {
                if (node.requirementEvaluations != null)
                {
                    foreach (var evaluation in node.requirementEvaluations)
                    {
                        if (evaluation.requirement != null)
                            items.Add(new GetGapScopeItemDto
                            {
                                tipo = "requisito",
                                itemId = evaluation.requirement.requirementId,
                                maturityLevelId = evaluation.maturityLevelId,
                            });
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
                    if (control.controlEvaluations == null) continue;
                    foreach (var evaluation in control.controlEvaluations)
                        items.Add(new GetGapScopeItemDto
                        {
                            tipo = "control",
                            itemId = control.controlId,
                            maturityLevelId = evaluation.maturityLevelId,
                        });
                }
            }
        }
    }
}
