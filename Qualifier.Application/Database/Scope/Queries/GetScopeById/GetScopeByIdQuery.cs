using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Scope.Queries.GetScopeById
{
    public class GetScopeByIdQuery : IGetScopeByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetScopeByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int scopeId)
        {
            try
            {
                var entity = await (from item in _databaseService.Scope
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.scopeId == scopeId)
                                    select new ScopeEntity()
                                    {
                                        scopeId = item.scopeId,
                                        isCurrent = item.isCurrent,
                                        date = item.date,
                                        name = item.name,
                                        description = item.description,
                                        standardId = item.standardId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetScopeByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

