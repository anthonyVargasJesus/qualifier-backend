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
                var risk = await (from item in _databaseService.Risk
                                  join activesInventory in _databaseService.ActivesInventory on item.activesInventoryId equals activesInventory.activesInventoryId
                                  where ((item.isDeleted == null || item.isDeleted == false) && item.riskId == riskId)
                                    select new RiskEntity()
                                    {
                                        riskId = item.riskId,
                                        name = item.name,
                                        evaluationId = item.evaluationId,
                                        activesInventoryId = item.activesInventoryId,
                                        activesInventoryNumber = item.activesInventoryNumber,
                                        activesInventoryName = item.activesInventoryName,
                                        menaceId = item.menaceId,
                                        vulnerabilityId = item.vulnerabilityId,
                                        companyId = item.companyId,
                                    }).FirstAsync();

                //var riskAssessment = await (from item in _databaseService.RiskAssessment
                //                      where ((item.isDeleted == null || item.isDeleted == false) && item.riskId == riskId)
                //                      select new RiskAssessmentEntity
                //                      {
                //                          riskAssessmentId = item.riskAssessmentId,
                //                      }).FirstOrDefaultAsync();

                var valuations = await (from valuationInActive in _databaseService.ValuationInActive
                                        where ((valuationInActive.isDeleted == null || valuationInActive.isDeleted == false)
                                        && valuationInActive.activesInventoryId == risk.activesInventoryId)
                                        select new ValuationInActiveEntity
                                        {
                                            value = valuationInActive.value,
                                        }).ToListAsync();

                var activeInventory = new ActivesInventoryEntity();
                activeInventory.setCID(valuations);

                var riskDto = _mapper.Map<GetRiskByIdDto>(risk);
                //if (riskAssessment != null)
                //    riskDto.riskAssessmentId = riskAssessment.riskAssessmentId;

                riskDto.valuationCID = activeInventory.valuationCID;

                //var riskTreatment = await (from item in _databaseService.RiskTreatment
                //                            where ((item.isDeleted == null || item.isDeleted == false) && item.riskId == riskId)
                //                            select new RiskTreatmentEntity
                //                            {
                //                                riskTreatmentId = item.riskTreatmentId,
                //                            }).FirstOrDefaultAsync();

                //if (riskTreatment != null)
                //    riskDto.riskTreatmentId = riskTreatment.riskTreatmentId;

                //var controlImplementation = await (from item in _databaseService.ControlImplementation
                //                           where ((item.isDeleted == null || item.isDeleted == false) && item.riskId == riskId)
                //                           select new ControlImplementationEntity
                //                           {
                //                               controlImplementationId = item.controlImplementationId,
                //                           }).FirstOrDefaultAsync();

                //if (controlImplementation != null)
                //    riskDto.controlImplementationId = controlImplementation.controlImplementationId;

                return riskDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

