using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskTreatmentMethod.Queries.GetRiskTreatmentMethodById
{
    public class GetRiskTreatmentMethodByIdQuery : IGetRiskTreatmentMethodByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskTreatmentMethodByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int riskTreatmentMethodId)
        {
            try
            {
                var entity = await (from item in _databaseService.RiskTreatmentMethod
                                    where ((item.isDeleted == null || item.isDeleted == false)
                                    && item.riskTreatmentMethodId == riskTreatmentMethodId)
                                    select new RiskTreatmentMethodEntity()
                                    {
                                        riskTreatmentMethodId = item.riskTreatmentMethodId,
                                        name = item.name,
                                        description = item.description,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetRiskTreatmentMethodByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

