using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Location.Queries.GetLocationsByCompanyId
{
    public class GetLocationsByCompanyIdQuery : IGetLocationsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetLocationsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from location in _databaseService.Location
                                      where ((location.isDeleted == null || location.isDeleted == false) && location.companyId == companyId)
                                      && (location.name.ToUpper().Contains(search.ToUpper()))
                                      select new LocationEntity
                                      {
                                          locationId = location.locationId,
                                          abbreviation = location.abbreviation,
                                          name = location.name,
                                          companyId = location.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetLocationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetLocationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetLocationsByCompanyIdDto>>(entities);
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
            var total = await (from location in _databaseService.Location
                               where ((location.isDeleted == null || location.isDeleted == false) && location.companyId == companyId)
                               && (location.name.ToUpper().Contains(search.ToUpper()))
                               select new LocationEntity
                               {
                                   locationId = location.locationId,
                               }).CountAsync();
            return total;
        }

    }
}

