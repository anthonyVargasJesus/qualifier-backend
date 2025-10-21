using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Risk.Queries.GetRiskMonitoring
{
    public class GetRiskMonitoringQuery : IGetRiskMonitoringQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskMonitoringQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search)
        {
            try
            {
                int evaluationId = 0;
                var currentEvaluation = await (from item in _databaseService.Evaluation
                                               where ((item.isDeleted == null || item.isDeleted == false) && item.isCurrent)
                                               select new EvaluationEntity()
                                               {
                                                   evaluationId = item.evaluationId,
                                               }).FirstOrDefaultAsync();

                if (currentEvaluation != null)
                    evaluationId = currentEvaluation.evaluationId;

                const int STATUS_IN_MONITORING_ID = 5;

                var risks = await (from risk in _databaseService.Risk
                                   join menace in _databaseService.Menace on risk.menaceId equals menace.menaceId
                                   join vulnerability in _databaseService.Vulnerability on risk.vulnerabilityId equals vulnerability.vulnerabilityId

                                   // LEFT JOIN con evaluación
                                   join assessment in _databaseService.RiskAssessment
                                       .Where(a => a.isDeleted == null || a.isDeleted == false)
                                   on risk.riskId equals assessment.riskId into riskAssessmentJoin
                                   from riskAssessment in riskAssessmentJoin.DefaultIfEmpty()

                                       // LEFT JOIN con RiskLevel (desde RiskAssessment)
                                   join level in _databaseService.RiskLevel
                                       on riskAssessment.riskLevelId equals level.riskLevelId into riskLevelJoin
                                   from riskLevel in riskLevelJoin.DefaultIfEmpty()

                                       // LEFT JOIN con tratamiento
                                   join treatment in _databaseService.RiskTreatment
                                       .Where(t => t.isDeleted == null || t.isDeleted == false)
                                   on risk.riskId equals treatment.riskId into riskTreatmentJoin
                                   from riskTreatment in riskTreatmentJoin.DefaultIfEmpty()

                                       // LEFT JOIN con RiskLevel (desde RiskAssessment)
                                   join level2 in _databaseService.RiskLevel
                                       on riskTreatment.riskLevelId equals level2.riskLevelId into riskLevelJoin2
                                   from riskLevel2 in riskLevelJoin2.DefaultIfEmpty()

                                       // INNER JOIN con controles implementados
                                   join control in _databaseService.ControlImplementation
                                       .Where(c => c.isDeleted == null || c.isDeleted == false)
                                   on risk.riskId equals control.riskId

                                   // Subconsultas: controles planificados y efectivos
                                   let plannedControls = _databaseService.ControlImplementation
                                                           .Count(ci => ci.riskId == risk.riskId && (ci.isDeleted == null || ci.isDeleted == false))
                                   let effectiveControls = _databaseService.ControlImplementation
                                                           .Count(ci => ci.riskId == risk.riskId && ci.isImplemented == true && (ci.isDeleted == null || ci.isDeleted == false))

                                   // Calcular el valor residual
                                   let residualRiskValue = (riskTreatment != null && riskAssessment != null && plannedControls > 0)
                                       ? riskAssessment.riskAssessmentValue -
                                         ((Convert.ToDecimal(effectiveControls) / Convert.ToDecimal(plannedControls)) *
                                         (riskAssessment.riskAssessmentValue - riskTreatment.riskAssessmentValue))
                                       : 0

                                   // Obtener el nivel de riesgo residual según el valor
                                   let residualLevel = _databaseService.RiskLevel
                                       .Where(rl => residualRiskValue >= rl.minimum && residualRiskValue <= rl.maximum)
                                       .FirstOrDefault()

                                   let trendText =
                                    (riskTreatment != null && residualRiskValue > riskTreatment.riskAssessmentValue) ? "Subiendo" :
                                    (riskTreatment != null && residualRiskValue < riskTreatment.riskAssessmentValue) ? "Bajando" :
                                    "Estable"

                                   let trendIcon =
                                       (riskTreatment != null && residualRiskValue > riskTreatment.riskAssessmentValue) ? "📈" :
                                       (riskTreatment != null && residualRiskValue < riskTreatment.riskAssessmentValue) ? "📉" :
                                       "➡️"

                                   where ((risk.isDeleted == null || risk.isDeleted == false)
                                          && risk.evaluationId == evaluationId
                                          && risk.riskStatusId == STATUS_IN_MONITORING_ID)
                                         && risk.name.ToUpper().Contains(search.ToUpper())

                                   select new RiskEntity
                                   {
                                       riskId = risk.riskId,
                                       name = risk.name,
                                       activesInventoryId = risk.activesInventoryId,
                                       activesInventoryNumber = risk.activesInventoryNumber,
                                       activesInventoryName = risk.activesInventoryName,
                                       evaluationId = evaluationId,
                                       menace = new MenaceEntity
                                       {
                                           name = menace.name,
                                       },
                                       vulnerability = new VulnerabilityEntity
                                       {
                                           name = vulnerability.name,
                                       },
                                       riskAssessmentId = (riskAssessment == null) ? 0 : riskAssessment.riskAssessmentId,
                                       riskTreatmentId = (riskTreatment == null) ? 0 : riskTreatment.riskTreatmentId,

                                       // Controles planificados y efectivos
                                       plannedControls = plannedControls,

                                       effectiveControls = effectiveControls,
                                       // Composición del texto "X / Y ✅" o "⚠️"
                                       controlSummary = string.Empty, // se completará luego o en la misma consulta

                                       initialRiskValue = (riskAssessment == null) ? 0 : riskAssessment.riskAssessmentValue,
                                       initialRiskLevel = (riskLevel == null) ? "" : riskLevel.name,
                                       initialRiskColor = (riskLevel == null) ? "" : riskLevel.color,

                                       treatedRiskValue = (riskTreatment == null) ? 0 : riskTreatment.riskAssessmentValue,
                                       treatedRiskLevel = (riskLevel2 == null) ? "" : riskLevel.name,
                                       treatedRiskColor = (riskLevel2 == null) ? "" : riskLevel.color,

                                       residualRiskValue = residualRiskValue,
                                       residualRiskLevel = residualLevel != null ? residualLevel.name : "",
                                       residualRiskColor = residualLevel != null ? residualLevel.color : "",

                                       trend = trendText,
                                       trendIcon = trendIcon
                                   })
                                   .Distinct() // evita duplicados por múltiples controles
                                   .Skip(skip)
                                   .Take(pageSize)
                                   .ToListAsync();

                foreach (var item in risks)
                {
                    item.controlSummary = $"{item.effectiveControls} / {item.plannedControls} " +
                        (item.effectiveControls == item.plannedControls
                            ? "✅"
                            : "⚠️");
                }

                BaseResponseDto<GetRiskMonitoringRiskDto> baseResponseDto = new BaseResponseDto<GetRiskMonitoringRiskDto>();
                baseResponseDto.data = _mapper.Map<List<GetRiskMonitoringRiskDto>>(risks);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, evaluationId), pageSize);

                return baseResponseDto;

            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int evaluationId)
        {
            const int IN_TREATMENT_STATUS_ID = 4;

            var total = await (from risk in _databaseService.Risk
                               join menace in _databaseService.Menace on risk.menaceId equals menace.menaceId
                               join vulnerability in _databaseService.Vulnerability on risk.vulnerabilityId equals vulnerability.vulnerabilityId

                               // LEFT JOIN con evaluación
                               join assessment in _databaseService.RiskAssessment
                                   .Where(a => a.isDeleted == null || a.isDeleted == false)
                               on risk.riskId equals assessment.riskId into riskAssessmentJoin
                               from riskAssessment in riskAssessmentJoin.DefaultIfEmpty()

                                   // LEFT JOIN con tratamiento
                               join treatment in _databaseService.RiskTreatment
                                   .Where(t => t.isDeleted == null || t.isDeleted == false)
                               on risk.riskId equals treatment.riskId into riskTreatmentJoin
                               from riskTreatment in riskTreatmentJoin.DefaultIfEmpty()

                                   // INNER JOIN con controles implementados
                               join control in _databaseService.ControlImplementation
                                   .Where(c => c.isDeleted == null || c.isDeleted == false)
                               on risk.riskId equals control.riskId

                               where ((risk.isDeleted == null || risk.isDeleted == false)
                                      && risk.evaluationId == evaluationId
                                      && risk.riskStatusId == IN_TREATMENT_STATUS_ID)
                                     && risk.name.ToUpper().Contains(search.ToUpper())

                               select new RiskEntity
                               {
                                   riskId = risk.riskId,
                               }).CountAsync();

            return total;
        }

    }
}
