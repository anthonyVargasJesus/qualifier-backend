using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Indicator.Queries.GetIndicatorsByCompanyId
{
    public class GetIndicatorsByCompanyIdQuery : IGetIndicatorsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetIndicatorsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from indicator in _databaseService.Indicator
                                      where ((indicator.isDeleted == null || indicator.isDeleted == false) && indicator.companyId == companyId)
                                      && (indicator.name.ToUpper().Contains(search.ToUpper()))
                                      select new IndicatorEntity
                                      {
                                          indicatorId = indicator.indicatorId,
                                          name = indicator.name,
                                          description = indicator.description,
                                          abbreviation = indicator.abbreviation,
                                          value = indicator.value,
                                          color = indicator.color,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetIndicatorsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetIndicatorsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetIndicatorsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from indicator in _databaseService.Indicator
                               where ((indicator.isDeleted == null || indicator.isDeleted == false) && indicator.companyId == companyId)
                               && (indicator.name.ToUpper().Contains(search.ToUpper()))
                               select new IndicatorEntity
                               {
                                   indicatorId = indicator.indicatorId,
                               }).CountAsync();
            return total;
        }

    }
}

