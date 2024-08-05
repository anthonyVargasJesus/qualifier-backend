using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IGetUserByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUserByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int userId)
        {
            try
            {
                var entity = await (from item in _databaseService.User
                                    join userState in _databaseService.UserState on item.userState equals userState
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.userId == userId)
                                    select new UserEntity()
                                    {
                                        userId = item.userId,
                                        name = item.name,
                                        middleName = item.middleName,
                                        firstName = item.firstName,
                                        lastName = item.lastName,
                                        email = item.email,
                                        phone = item.phone,
                                        password = item.password,
                                        userStateId = item.userStateId,
                                        image = item.image,
                                        documentNumber = item.documentNumber,
                                        companyId = item.companyId,
                                        standardId = item.standardId,
                                        userState = new UserStateEntity
                                        {
                                            name = userState.name,
                                        },
                                    }).FirstOrDefaultAsync();

                var roles = await (from roleInUser in _databaseService.RoleInUser
                                   join role in _databaseService.Role on roleInUser.role equals role
                                   where ((roleInUser.isDeleted == null || roleInUser.isDeleted == false) && roleInUser.userId == userId)
                                   select new RoleEntity
                                   {
                                       roleId = roleInUser.roleId,
                                       name = role.name,

                                   }).ToListAsync();


                if (entity != null)
                    entity.roles = roles;


                return _mapper.Map<GetUserByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

