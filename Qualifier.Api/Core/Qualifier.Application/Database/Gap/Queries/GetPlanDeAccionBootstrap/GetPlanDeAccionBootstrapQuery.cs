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
                // IDatabaseService (el DbContext de EF Core) está registrado como
                // Scoped: todas las queries de este request comparten la misma
                // instancia, que no soporta operaciones concurrentes. Por eso acá
                // adentro se resuelve todo con await secuencial (no Task.WhenAll) —
                // la ganancia real de este endpoint es juntar las 7 llamadas HTTP
                // del cliente en una sola, no paralelizar contra la base de datos.
                var evalResult = await _getCurrentEvaluationQuery.Execute(0);
                if (evalResult is BaseErrorResponseDto) return evalResult;
                if (evalResult is not GetCurrentEvaluationDto evaluation) return BaseApplication.getExceptionErrorResponse();

                var maturityResult = await _getAllMaturityLevelsByCompanyIdQuery.Execute(companyId);
                if (maturityResult is BaseErrorResponseDto) return maturityResult;

                var statusesResult = await _getAllActionPlanStatussByCompanyIdQuery.Execute(companyId);
                if (statusesResult is BaseErrorResponseDto) return statusesResult;

                var prioritiesResult = await _getAllActionPlanPrioritiesByCompanyIdQuery.Execute(companyId);
                if (prioritiesResult is BaseErrorResponseDto) return prioritiesResult;

                var usersResult = await _getAllUsersByCompanyIdQuery.Execute(companyId);
                if (usersResult is BaseErrorResponseDto) return usersResult;

                var reqResult = await _getRequirementEvaluationByProcessQuery.Execute(standardId, evaluation.evaluationId, string.Empty, userId, scopeToUser);
                if (reqResult is BaseErrorResponseDto) return reqResult;

                var ctrlResult = await _getControlEvaluationByProcessQuery.Execute(standardId, evaluation.evaluationId, userId, scopeToUser);
                if (ctrlResult is BaseErrorResponseDto) return ctrlResult;

                var breachesResult = await _getBreachesScopeQuery.Execute(evaluation.evaluationId);
                if (breachesResult is BaseErrorResponseDto) return breachesResult;

                var items = new List<GetGapScopeItemDto>();
                if (reqResult is GetRequirementEvaluationsByProcessResponseDto reqDto && reqDto.requirements != null)
                    CollectRequirementItems(reqDto.requirements, items);
                if (ctrlResult is GetControlEvaluationsByProcessResponseDto ctrlDto && ctrlDto.groups != null)
                    CollectControlItems(ctrlDto.groups, items);

                var breaches = ((BaseResponseDto<Breach.Queries.GetBreachesScope.GetBreachesScopeDto>)breachesResult).data;
                var maturityLevels = ((BaseResponseDto<GetAllMaturityLevelsByCompanyIdDto>)maturityResult).data;
                var statuses = ((BaseResponseDto<GetAllActionPlanStatussByCompanyIdDto>)statusesResult).data;
                var priorities = ((BaseResponseDto<GetAllActionPlanPrioritiesByCompanyIdDto>)prioritiesResult).data;
                var users = ((BaseResponseDto<GetAllUsersByCompanyIdDto>)usersResult).data;

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
                                theme = "Cláusulas",
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
                            theme = group.name,
                        });
                }
            }
        }
    }
}
