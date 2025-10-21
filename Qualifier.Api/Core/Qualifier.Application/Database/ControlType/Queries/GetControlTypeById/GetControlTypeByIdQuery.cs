using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlType.Queries.GetControlTypeById
{
    public class GetControlTypeByIdQuery : IGetControlTypeByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlTypeByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int controlTypeId)
        {
            try
            {
                var entity = await (from item in _databaseService.ControlType
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.controlTypeId == controlTypeId)
                                    select new ControlTypeEntity()
                                    {
                                        controlTypeId = item.controlTypeId,
                                        name = item.name,
                                        description = item.description,
                                        abbreviation = item.abbreviation,
                                        factor = item.factor,
                                        minimum = item.minimum,
                                        maximum = item.maximum,
                                        color = item.color,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetControlTypeByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

