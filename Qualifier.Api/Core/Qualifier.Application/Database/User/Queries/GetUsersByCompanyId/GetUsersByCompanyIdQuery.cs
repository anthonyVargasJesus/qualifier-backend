using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.User.Queries.GetUsersByCompanyId
{
    public class GetUsersByCompanyIdQuery : IGetUsersByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUsersByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var users = await (from user in _databaseService.User
                                      join userState in _databaseService.UserState on user.userState equals userState
                                      where ((user.isDeleted == null || user.isDeleted == false) && user.companyId == companyId)
                                      && (user.name.ToUpper().Contains(search.ToUpper()) || user.firstName.ToUpper().Contains(search.ToUpper())
                                      || user.lastName.ToUpper().Contains(search.ToUpper()))
                                      select new UserEntity
                                      {
                                          userId = user.userId,
                                          name = user.name,
                                          middleName = user.middleName,
                                          firstName = user.firstName,
                                          lastName = user.lastName,
                                          email = user.email,
                                          phone = user.phone,
                                          userStateId = user.userStateId,
                                          image = user.image,
                                          documentNumber = user.documentNumber,
                                          lastAccess = user.lastAccess,
                                          userState = new UserStateEntity
                                          {
                                              name = userState.name,
                                              value = userState.value,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                var userIds = users.Select(x => x.userId).ToList();

                var roles = await (from ru in _databaseService.RoleInUser
                                   join r in _databaseService.Role
                                        on ru.roleId equals r.roleId
                                   where userIds.Contains(ru.userId)
                                   select new
                                   {
                                       ru.userId,
                                       r.name
                                   }).ToListAsync();

                foreach (var user in users)
                {
                   
                   var userRoles = roles.Where(x => x.userId == user.userId).Select(x => x.name).ToList();

                    if (userRoles.Count > 2)
                    {
                        // Toma los primeros 3 y añade "..."
                        user.rolesText = string.Join(", ", userRoles.Take(2)) + "...";
                    }
                    else
                    {
                        // Une todos si son 3 o menos
                        user.rolesText = string.Join(", ", userRoles);
                    }

                    if (userRoles.Count == 0)
                        user.rolesText = "Sin roles asignados";

                }

                BaseResponseDto<GetUsersByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetUsersByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetUsersByCompanyIdDto>>(users);
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
            var total = await (from user in _databaseService.User
                               where ((user.isDeleted == null || user.isDeleted == false) && user.companyId == companyId)
                                && (user.name.ToUpper().Contains(search.ToUpper()) || user.firstName.ToUpper().Contains(search.ToUpper())
                                      || user.lastName.ToUpper().Contains(search.ToUpper()))
                               select new UserEntity
                               {
                                   userId = user.userId,
                               }).CountAsync();
            return total;
        }

    }
}

