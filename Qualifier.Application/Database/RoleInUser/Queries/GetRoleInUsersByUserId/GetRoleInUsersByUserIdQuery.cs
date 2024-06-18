using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUsersByUserId
{
    public class GetRoleInUsersByUserIdQuery : IGetRoleInUsersByUserIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRoleInUsersByUserIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int userId)
        {
            try
            {
                var entities = await (from roleInUser in _databaseService.RoleInUser
                                      join role in _databaseService.Role on roleInUser.role equals role
                                      where ((roleInUser.isDeleted == null || roleInUser.isDeleted == false) && roleInUser.userId == userId)
                                      && (roleInUser.role.name.ToUpper().Contains(search.ToUpper()))
                                      select new RoleInUserEntity
                                      {
                                          roleInUserId = roleInUser.roleInUserId,
                                          roleId = roleInUser.roleId,
                                          role = new RoleEntity
                                          {
                                              name = role.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetRoleInUsersByUserIdDto> baseResponseDto = new BaseResponseDto<GetRoleInUsersByUserIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRoleInUsersByUserIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, userId), pageSize);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int userId)
        {
            var total = await (from roleInUser in _databaseService.RoleInUser
                               join role in _databaseService.Role on roleInUser.role equals role
                               where ((roleInUser.isDeleted == null || roleInUser.isDeleted == false) && roleInUser.userId == userId)
                               && (roleInUser.role.name.ToUpper().Contains(search.ToUpper()))
                               select new RoleInUserEntity
                               {
                                   roleInUserId = roleInUser.roleInUserId,
                               }).CountAsync();
            return total;
        }

    }
}

