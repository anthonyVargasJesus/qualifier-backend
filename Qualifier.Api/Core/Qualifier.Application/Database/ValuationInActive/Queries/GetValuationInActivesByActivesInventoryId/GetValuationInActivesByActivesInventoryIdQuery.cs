using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActivesByActivesInventoryId
{
    public class GetValuationInActivesByActivesInventoryIdQuery : IGetValuationInActivesByActivesInventoryIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetValuationInActivesByActivesInventoryIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int activesInventoryId)
        {
            try
            {
                var entities = await (from valuationInActive in _databaseService.ValuationInActive
                                      join impactValuation in _databaseService.ImpactValuation on valuationInActive.impactValuation equals impactValuation
                                      where ((valuationInActive.isDeleted == null || valuationInActive.isDeleted == false) && valuationInActive.activesInventoryId == activesInventoryId)
                                      && (impactValuation.name.ToUpper().Contains(search.ToUpper()))
                                      select new ValuationInActiveEntity
                                      {
                                          valuationInActiveId = valuationInActive.valuationInActiveId,
                                          activesInventoryId = valuationInActive.activesInventoryId,
                                          impactValuationId = valuationInActive.impactValuationId,
                                          value = valuationInActive.value,
                                          companyId = valuationInActive.companyId,
                                          impactValuation = new ImpactValuationEntity
                                          {
                                              name = impactValuation.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetValuationInActivesByActivesInventoryIdDto> baseResponseDto = new BaseResponseDto<GetValuationInActivesByActivesInventoryIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetValuationInActivesByActivesInventoryIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, activesInventoryId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int activesInventoryId)
        {
            var total = await (from valuationInActive in _databaseService.ValuationInActive
                               join impactValuation in _databaseService.ImpactValuation on valuationInActive.impactValuation equals impactValuation
                               where ((valuationInActive.isDeleted == null || valuationInActive.isDeleted == false) && valuationInActive.activesInventoryId == activesInventoryId)
                               && (impactValuation.name.ToUpper().Contains(search.ToUpper()))
                               select new ValuationInActiveEntity
                               {
                                   valuationInActiveId = valuationInActive.valuationInActiveId,
                               }).CountAsync();
            return total;
        }

    }
}

