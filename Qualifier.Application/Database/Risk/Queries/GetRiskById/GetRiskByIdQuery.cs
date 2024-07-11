using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Risk.Queries.GetRiskById
{
    public class GetRiskByIdQuery : IGetRiskByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int riskId)
        {
            try
            {
                var entity = await (from item in _databaseService.Risk
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.riskId == riskId)
                                    select new RiskEntity()
                                    {
                                        riskId = item.riskId,
                                        activesInventoryId = item.activesInventoryId,
                                        activesInventoryNumber = item.activesInventoryNumber,
                                        macroProcess = item.macroProcess,
                                        subProcess = item.subProcess,
                                        activesInventoryName = item.activesInventoryName,
                                        activesInventoryValuation = item.activesInventoryValuation,
                                        menaceId = item.menaceId,
                                        vulnerabilityId = item.vulnerabilityId,
                                        menaceLevel = item.menaceLevel,
                                        vulnerabilityLevel = item.vulnerabilityLevel,
                                        controlId = item.controlId,
                                        riskAssessmentValue = item.riskAssessmentValue,
                                        riskLevelId = item.riskLevelId,
                                        treatmentMethod = item.treatmentMethod,
                                        controlTypeId = item.controlTypeId,
                                        controlsToImplement = item.controlsToImplement,
                                        menaceLevelWithTreatment = item.menaceLevelWithTreatment,
                                        vulnerabilityLevelWithTreatment = item.vulnerabilityLevelWithTreatment,
                                        riskAssessmentValueWithTreatment = item.riskAssessmentValueWithTreatment,
                                        riskLevelWithImplementedControlld = item.riskLevelWithImplementedControlld,
                                        residualRisk = item.residualRisk,
                                        activities = item.activities,
                                        implementationStartDate = item.implementationStartDate,
                                        verificationDate = item.verificationDate,
                                        responsibleId = item.responsibleId,
                                        observation = item.observation,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetRiskByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

