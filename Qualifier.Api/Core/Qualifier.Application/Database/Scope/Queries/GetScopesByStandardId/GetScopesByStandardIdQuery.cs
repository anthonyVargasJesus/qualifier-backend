using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Scope.Queries.GetScopesByStandardId
{
    public class GetScopesByStandardIdQuery : IGetScopesByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetScopesByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int standardId)
        {
            try
            {
                var entities = await (from scope in _databaseService.Scope
                                      where ((scope.isDeleted == null || scope.isDeleted == false) && scope.standardId == standardId)
                                      && (scope.name.ToUpper().Contains(search.ToUpper()))
                                      select new ScopeEntity
                                      {
                                          scopeId = scope.scopeId,
                                          isCurrent = scope.isCurrent,
                                          date = scope.date,
                                          name = scope.name,
                                          description = scope.description,
                                          standardId = scope.standardId,
                                          companyId = scope.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetScopesByStandardIdDto> baseResponseDto = new BaseResponseDto<GetScopesByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetScopesByStandardIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, standardId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
               return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int standardId)
        {
            var total = await (from scope in _databaseService.Scope
                               where ((scope.isDeleted == null || scope.isDeleted == false) && scope.standardId == standardId)
                               && (scope.name.ToUpper().Contains(search.ToUpper()))
                               select new ScopeEntity
                               {
                                   scopeId = scope.scopeId,
                               }).CountAsync();
            return total;
        }

    }
}

