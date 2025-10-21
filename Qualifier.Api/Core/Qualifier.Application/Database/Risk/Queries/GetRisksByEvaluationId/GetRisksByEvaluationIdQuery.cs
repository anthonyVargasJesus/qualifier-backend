using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Risk.Queries.GetRisksByEvaluationId
{
    public class GetRisksByEvaluationIdQuery : IGetRisksByEvaluationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRisksByEvaluationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int riskStatusId)
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

                var entity = await (from item in _databaseService.Evaluation
                                    join standard in _databaseService.Standard on item.standard equals standard
                                    join evaluationState in _databaseService.EvaluationState on item.evaluationState equals evaluationState
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.isCurrent)
                                    select new EvaluationEntity()
                                    {
                                        evaluationId = item.evaluationId,
                                        startDate = item.startDate,
                                        endDate = item.endDate,
                                        description = item.description,
                                        referenceEvaluationId = item.referenceEvaluationId,
                                        isGapAnalysis = item.isGapAnalysis,
                                        standardId = standard.standardId,
                                        isCurrent = item.isCurrent,
                                        evaluationState = new EvaluationStateEntity
                                        {
                                            name = evaluationState.name,
                                            color = evaluationState.color,
                                        },
                                        standard = new StandardEntity
                                        {
                                            name = standard.name,
                                        },
                                    }).FirstOrDefaultAsync();

                var risks = await (from risk in _databaseService.Risk
                                      join menace in _databaseService.Menace on risk.menace equals menace
                                      join vulnerability in _databaseService.Vulnerability on risk.vulnerability equals vulnerability

                                      join assessment in _databaseService.RiskAssessment
                                        .Where(a => a.isDeleted == null || a.isDeleted == false)
                                      on risk.riskId equals assessment.riskId into riskAssessmentJoin
                                      from riskAssessment in riskAssessmentJoin.DefaultIfEmpty() // LEFT JOIN

                                      join treatment in _databaseService.RiskTreatment
                                        .Where(a => a.isDeleted == null || a.isDeleted == false)
                                      on risk.riskId equals treatment.riskId into riskTreatmentJoin
                                      from riskTreatment in riskTreatmentJoin.DefaultIfEmpty() // LEFT JOIN

                                      where ((risk.isDeleted == null || risk.isDeleted == false) && risk.evaluationId == evaluationId
                                      && risk.riskStatusId == riskStatusId)
                                      && (risk.name.ToUpper().Contains(search.ToUpper()))
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
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                var controls = await (from controlImplementation in _databaseService.ControlImplementation
                                      join bt in _databaseService.Risk on controlImplementation.riskId equals bt.riskId into _bt
                                      from risk in _bt.DefaultIfEmpty()
                                      where (controlImplementation.isDeleted == null || controlImplementation.isDeleted == false)
                                      && (risk.evaluationId == evaluationId)
                                      select new ControlImplementationEntity
                                      {
                                          controlImplementationId = controlImplementation.controlImplementationId,
                                          riskId = risk.riskId,
                                          isImplemented = controlImplementation.isImplemented == null ? false : controlImplementation.isImplemented,
                                      }).ToListAsync();

                foreach (var risk in risks)
                {
                    var associatedControls = controls.Where(c => c.riskId == risk.riskId).ToList();

                    var total = associatedControls.Count;
                    var implemented = associatedControls.Count(c => c.isImplemented == true);
                    var pending = total - implemented;

                    if (total > 0)
                    {
                        risk.controlSummary = $"{total} control{(total > 1 ? "es" : "")} ({implemented} implementado{(implemented != 1 ? "s" : "")}, {pending} pendiente{(pending != 1 ? "s" : "")})";
                    }
                    else
                    {
                        risk.controlSummary = "Sin controles";
                    }
                }

                GetRisksByEvaluationIdResponse<GetRisksByEvaluationIdDto> baseResponseDto = new GetRisksByEvaluationIdResponse<GetRisksByEvaluationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRisksByEvaluationIdDto>>(risks);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, evaluationId, riskStatusId), pageSize);
                baseResponseDto.evaluationId = evaluationId;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int evaluationId, int riskStatusId)
        {
            var total = await (from risk in _databaseService.Risk
                               join menace in _databaseService.Menace on risk.menace equals menace
                               join vulnerability in _databaseService.Vulnerability on risk.vulnerability equals vulnerability
                               where ((risk.isDeleted == null || risk.isDeleted == false) && risk.evaluationId == evaluationId
                               && risk.riskStatusId == riskStatusId)
                               && (risk.name.ToUpper().Contains(search.ToUpper()))
                               select new RiskEntity
                               {
                                   riskId = risk.riskId,
                               }).CountAsync();
            return total;
        }

    }
}
