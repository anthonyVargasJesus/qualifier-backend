using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess;
using Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Gap.Queries.GetEvaluacionBootstrap
{
    public class GetEvaluacionBootstrapQuery : IGetEvaluacionBootstrapQuery
    {
        private readonly IGetCurrentEvaluationQuery _getCurrentEvaluationQuery;
        private readonly IGetAllMaturityLevelsByCompanyIdQuery _getAllMaturityLevelsByCompanyIdQuery;
        private readonly IGetAllResponsiblesByStandardIdQuery _getAllResponsiblesByStandardIdQuery;
        private readonly IGetRequirementEvaluationByProcessQuery _getRequirementEvaluationByProcessQuery;
        private readonly IGetControlEvaluationByProcessQuery _getControlEvaluationByProcessQuery;

        public GetEvaluacionBootstrapQuery(
            IGetCurrentEvaluationQuery getCurrentEvaluationQuery,
            IGetAllMaturityLevelsByCompanyIdQuery getAllMaturityLevelsByCompanyIdQuery,
            IGetAllResponsiblesByStandardIdQuery getAllResponsiblesByStandardIdQuery,
            IGetRequirementEvaluationByProcessQuery getRequirementEvaluationByProcessQuery,
            IGetControlEvaluationByProcessQuery getControlEvaluationByProcessQuery)
        {
            _getCurrentEvaluationQuery = getCurrentEvaluationQuery;
            _getAllMaturityLevelsByCompanyIdQuery = getAllMaturityLevelsByCompanyIdQuery;
            _getAllResponsiblesByStandardIdQuery = getAllResponsiblesByStandardIdQuery;
            _getRequirementEvaluationByProcessQuery = getRequirementEvaluationByProcessQuery;
            _getControlEvaluationByProcessQuery = getControlEvaluationByProcessQuery;
        }

        public async Task<Object> Execute(int companyId, int userId)
        {
            try
            {
                // IDatabaseService es Scoped: todas las queries de este request
                // comparten el mismo DbContext, que no soporta operaciones
                // concurrentes — await secuencial, no Task.WhenAll (mismo motivo
                // que en GetPlanDeAccionBootstrapQuery y GetMyActionsBootstrapQuery).
                var evalResult = await _getCurrentEvaluationQuery.Execute(0);
                if (evalResult is BaseErrorResponseDto) return evalResult;
                if (evalResult is not GetCurrentEvaluationDto evaluation) return BaseApplication.getExceptionErrorResponse();

                var maturityResult = await _getAllMaturityLevelsByCompanyIdQuery.Execute(companyId);
                if (maturityResult is BaseErrorResponseDto) return maturityResult;

                var responsiblesResult = await _getAllResponsiblesByStandardIdQuery.Execute(evaluation.standardId);
                if (responsiblesResult is BaseErrorResponseDto) return responsiblesResult;

                // scopeToUser=true: mismo valor fijo que el frontend ya mandaba
                // siempre para esta pantalla (ver getGapEvaluationData()).
                var reqResult = await _getRequirementEvaluationByProcessQuery.Execute(evaluation.standardId, evaluation.evaluationId, string.Empty, userId, true);
                if (reqResult is BaseErrorResponseDto) return reqResult;

                var ctrlResult = await _getControlEvaluationByProcessQuery.Execute(evaluation.standardId, evaluation.evaluationId, userId, true);
                if (ctrlResult is BaseErrorResponseDto) return ctrlResult;

                var maturityLevels = ((BaseResponseDto<GetAllMaturityLevelsByCompanyIdDto>)maturityResult).data;
                var responsibles = ((BaseResponseDto<GetAllResponsiblesByStandardIdDto>)responsiblesResult).data;
                var requirements = reqResult is GetRequirementEvaluationsByProcessResponseDto reqDto
                    ? reqDto.requirements ?? new List<GetRequirementEvaluationsByProcessRequirementDto>()
                    : new List<GetRequirementEvaluationsByProcessRequirementDto>();
                var groups = ctrlResult is GetControlEvaluationsByProcessResponseDto ctrlDto
                    ? ctrlDto.groups ?? new List<GetControlEvaluationsByProcessControlGroupDto>()
                    : new List<GetControlEvaluationsByProcessControlGroupDto>();

                var response = new GetEvaluacionBootstrapDto
                {
                    evaluation = evaluation,
                    maturityLevels = maturityLevels,
                    responsibles = responsibles,
                    requirements = requirements,
                    groups = groups,
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
