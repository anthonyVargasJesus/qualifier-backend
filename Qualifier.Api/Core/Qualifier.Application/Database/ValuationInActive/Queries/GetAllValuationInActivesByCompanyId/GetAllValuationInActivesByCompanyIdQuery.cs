using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ValuationInActive.Queries.GetAllValuationInActivesByCompanyId
{
    internal class GetAllValuationInActivesByCompanyIdQuery : IGetAllValuationInActivesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllValuationInActivesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from valuationInActive in _databaseService.ValuationInActive
                                      join impactValuation in _databaseService.ImpactValuation on valuationInActive.impactValuation equals impactValuation
                                      where ((valuationInActive.isDeleted == null || valuationInActive.isDeleted == false) && valuationInActive.companyId == companyId)
                                      select new ValuationInActiveEntity
                                      {
                                          valuationInActiveId = valuationInActive.valuationInActiveId,
                                          activesInventoryId = valuationInActive.activesInventoryId,
                                          impactValuationId = valuationInActive.impactValuationId,
                                          value = valuationInActive.value,
                                          companyId = valuationInActive.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllValuationInActivesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllValuationInActivesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllValuationInActivesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

