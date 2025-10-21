using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Location.Queries.GetAllLocationsByCompanyId
{
    internal class GetAllLocationsByCompanyIdQuery : IGetAllLocationsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllLocationsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from location in _databaseService.Location
                                      where ((location.isDeleted == null || location.isDeleted == false) && location.companyId == companyId)
                                      select new LocationEntity
                                      {
                                          locationId = location.locationId,
                                          abbreviation = location.abbreviation,
                                          name = location.name,
                                          companyId = location.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllLocationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllLocationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllLocationsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

