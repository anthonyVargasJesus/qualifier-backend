using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Control.Queries.GetControlById
{
    public class GetControlByIdQuery : IGetControlByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int controlId)
        {
            try
            {
                var entity = await (from item in _databaseService.Control
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.controlId == controlId)
                                    select new ControlEntity()
                                    {
                                        controlId = item.controlId,
                                        number = item.number,
                                        name = item.name,
                                        description = item.description,
                                        controlGroupId = item.controlGroupId,
                                        standardId = item.standardId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetControlByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

