using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Indicator.Queries.GetIndicatorById
{
    public class GetIndicatorByIdQuery : IGetIndicatorByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetIndicatorByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int indicatorId)
        {
            try
            {
                var entity = await (from item in _databaseService.Indicator
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.indicatorId == indicatorId)
                                    select new IndicatorEntity()
                                    {
                                        indicatorId = item.indicatorId,
                                        name = item.name,
                                        description = item.description,
                                        abbreviation = item.abbreviation,
                                        minimum = item.minimum,
                                        maximum = item.maximum,
                                        color = item.color,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetIndicatorByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}

