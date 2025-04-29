using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskTreatmentMethod.Queries.GetRiskTreatmentMethodsByCompanyId
{
    public class GetRiskTreatmentMethodsByCompanyIdQuery : IGetRiskTreatmentMethodsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskTreatmentMethodsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from riskTreatmentMethod in _databaseService.RiskTreatmentMethod
                                      where ((riskTreatmentMethod.isDeleted == null || riskTreatmentMethod.isDeleted == false) && riskTreatmentMethod.companyId == companyId)
                                      && (riskTreatmentMethod.name.ToUpper().Contains(search.ToUpper()))
                                      select new RiskTreatmentMethodEntity
                                      {
                                          riskTreatmentMethodId = riskTreatmentMethod.riskTreatmentMethodId,
                                          name = riskTreatmentMethod.name,
                                          description = riskTreatmentMethod.description,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetRiskTreatmentMethodsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetRiskTreatmentMethodsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRiskTreatmentMethodsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
               return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from riskTreatmentMethod in _databaseService.RiskTreatmentMethod
                               where ((riskTreatmentMethod.isDeleted == null || riskTreatmentMethod.isDeleted == false) && riskTreatmentMethod.companyId == companyId)
                               && (riskTreatmentMethod.name.ToUpper().Contains(search.ToUpper()))
                               select new RiskTreatmentMethodEntity
                               {
                                   riskTreatmentMethodId = riskTreatmentMethod.riskTreatmentMethodId,
                               }).CountAsync();
            return total;
        }

    }
}

