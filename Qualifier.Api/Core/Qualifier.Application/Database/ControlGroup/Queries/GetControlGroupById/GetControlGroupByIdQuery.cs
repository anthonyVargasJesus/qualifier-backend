using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupById
{
    public class GetControlGroupByIdQuery : IGetControlGroupByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlGroupByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int controlGroupId)
        {
            try
            {
                var entity = await (from item in _databaseService.ControlGroup
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.controlGroupId == controlGroupId)
                                    select new ControlGroupEntity()
                                    {
                                        controlGroupId = item.controlGroupId,
                                        number = item.number,
                                        name = item.name,
                                        description = item.description,
                                        standardId = item.standardId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetControlGroupByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

