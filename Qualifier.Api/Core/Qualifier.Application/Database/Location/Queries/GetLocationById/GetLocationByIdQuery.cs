using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Location.Queries.GetLocationById
{
    public class GetLocationByIdQuery : IGetLocationByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetLocationByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int locationId)
        {
            try
            {
                var entity = await (from item in _databaseService.Location
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.locationId == locationId)
                                    select new LocationEntity()
                                    {
                                        locationId = item.locationId,
                                        abbreviation = item.abbreviation,
                                        name = item.name,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetLocationByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

