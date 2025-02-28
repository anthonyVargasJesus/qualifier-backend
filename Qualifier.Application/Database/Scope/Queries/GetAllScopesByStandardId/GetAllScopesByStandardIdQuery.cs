using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Scope.Queries.GetAllScopesByStandardId
{
    internal class GetAllScopesByStandardIdQuery : IGetAllScopesByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllScopesByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId)
        {
            try
            {
                var entities = await (from scope in _databaseService.Scope
                                      where ((scope.isDeleted == null || scope.isDeleted == false) && scope.standardId == standardId)
                                      select new ScopeEntity
                                      {
                                          scopeId = scope.scopeId,
                                          isCurrent = scope.isCurrent,
                                          date = scope.date,
                                          name = scope.name,
                                          description = scope.description,
                                          standardId = scope.standardId,
                                          companyId = scope.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllScopesByStandardIdDto> baseResponseDto = new BaseResponseDto<GetAllScopesByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllScopesByStandardIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

