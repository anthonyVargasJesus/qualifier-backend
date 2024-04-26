using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlById
{
    public class GetSupportForControlByIdQuery : IGetSupportForControlByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSupportForControlByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int supportForControlId)
        {
            try
            {
                var entity = await (from item in _databaseService.SupportForControl
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.supportForControlId == supportForControlId)
                                    select new SupportForControlEntity()
                                    {
                                        supportForControlId = item.supportForControlId,
                                        documentationId = item.documentationId,
                                        controlId = item.controlId,
                                        standardId = item.standardId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetSupportForControlByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

