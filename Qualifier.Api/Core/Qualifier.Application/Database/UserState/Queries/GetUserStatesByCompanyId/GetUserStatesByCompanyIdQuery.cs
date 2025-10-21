using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UserState.Queries.GetUserStatesByCompanyId
{
    public class GetUserStatesByCompanyIdQuery : IGetUserStatesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUserStatesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from userState in _databaseService.UserState
                                      where ((userState.isDeleted == null || userState.isDeleted == false) && userState.companyId == companyId)
                                      && (userState.name.ToUpper().Contains(search.ToUpper()))
                                      select new UserStateEntity
                                      {
                                          userStateId = userState.userStateId,
                                          name = userState.name,
                                          value = userState.value,
                                          companyId = userState.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetUserStatesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetUserStatesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetUserStatesByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from userState in _databaseService.UserState
                               where ((userState.isDeleted == null || userState.isDeleted == false) && userState.companyId == companyId)
                               && (userState.name.ToUpper().Contains(search.ToUpper()))
                               select new UserStateEntity
                               {
                                   userStateId = userState.userStateId,
                               }).CountAsync();
            return total;
        }

    }
}

