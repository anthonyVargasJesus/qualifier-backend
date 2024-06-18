using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUserById
{
    public class GetRoleInUserByIdQuery : IGetRoleInUserByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRoleInUserByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int roleInUserId)
        {
            try
            {
                var entity = await (from item in _databaseService.RoleInUser
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.roleInUserId == roleInUserId)
                                    select new RoleInUserEntity()
                                    {
                                        roleInUserId = item.roleInUserId,
                                        roleId = item.roleId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetRoleInUserByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

