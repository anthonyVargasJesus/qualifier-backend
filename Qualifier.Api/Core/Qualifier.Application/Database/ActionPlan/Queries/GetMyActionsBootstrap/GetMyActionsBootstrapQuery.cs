using Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByUserId;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetAllActionPlanStatussByCompanyId;
using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetMyActionsBootstrap
{
    public class GetMyActionsBootstrapQuery : IGetMyActionsBootstrapQuery
    {
        private readonly IGetCurrentEvaluationQuery _getCurrentEvaluationQuery;
        private readonly IGetActionPlansByUserIdQuery _getActionPlansByUserIdQuery;
        private readonly IGetAllActionPlanStatussByCompanyIdQuery _getAllActionPlanStatussByCompanyIdQuery;

        public GetMyActionsBootstrapQuery(
            IGetCurrentEvaluationQuery getCurrentEvaluationQuery,
            IGetActionPlansByUserIdQuery getActionPlansByUserIdQuery,
            IGetAllActionPlanStatussByCompanyIdQuery getAllActionPlanStatussByCompanyIdQuery)
        {
            _getCurrentEvaluationQuery = getCurrentEvaluationQuery;
            _getActionPlansByUserIdQuery = getActionPlansByUserIdQuery;
            _getAllActionPlanStatussByCompanyIdQuery = getAllActionPlanStatussByCompanyIdQuery;
        }

        public async Task<Object> Execute(int companyId, int userId)
        {
            try
            {
                // IDatabaseService es Scoped: todas las queries de este request
                // comparten el mismo DbContext, que no soporta operaciones
                // concurrentes — por eso acá adentro se resuelve todo con await
                // secuencial (ver el mismo ajuste ya hecho en
                // GetPlanDeAccionBootstrapQuery).
                var evalResult = await _getCurrentEvaluationQuery.Execute(0);
                if (evalResult is BaseErrorResponseDto) return evalResult;
                if (evalResult is not GetCurrentEvaluationDto evaluation) return BaseApplication.getExceptionErrorResponse();

                var tasksResult = await _getActionPlansByUserIdQuery.Execute(userId, evaluation.evaluationId);
                if (tasksResult is BaseErrorResponseDto) return tasksResult;

                var statusesResult = await _getAllActionPlanStatussByCompanyIdQuery.Execute(companyId);
                if (statusesResult is BaseErrorResponseDto) return statusesResult;

                var tasks = ((BaseResponseDto<GetActionPlansByUserIdDto>)tasksResult).data;
                var statuses = ((BaseResponseDto<GetAllActionPlanStatussByCompanyIdDto>)statusesResult).data;

                var response = new GetMyActionsBootstrapDto
                {
                    tasks = tasks,
                    statuses = statuses,
                };
                return response;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
