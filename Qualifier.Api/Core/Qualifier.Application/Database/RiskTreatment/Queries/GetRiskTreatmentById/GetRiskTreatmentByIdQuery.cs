using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskTreatment.Queries.GetRiskTreatmentById
{
    public class GetRiskTreatmentByIdQuery : IGetRiskTreatmentByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskTreatmentByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int riskTreatmentId)
        {
            try
            {
                var entity = await (from item in _databaseService.RiskTreatment
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.riskTreatmentId == riskTreatmentId)
                                    select new RiskTreatmentEntity()
                                    {
                                        riskTreatmentId = item.riskTreatmentId,
                                        riskId = item.riskId,
                                        riskTreatmentMethodId = item.riskTreatmentMethodId,
                                        controlType = item.controlType,
                                        controlsToImplement = item.controlsToImplement,
                                        menaceLevelValue = item.menaceLevelValue,
                                        vulnerabilityLevelValue = item.vulnerabilityLevelValue,
                                        riskAssessmentValue = item.riskAssessmentValue,
                                        riskLevelId = item.riskLevelId,
                                        residualRiskId = item.residualRiskId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();



                return _mapper.Map<GetRiskTreatmentByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

