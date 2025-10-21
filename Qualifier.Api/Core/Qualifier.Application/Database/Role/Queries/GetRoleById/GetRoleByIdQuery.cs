using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Role.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IGetRoleByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRoleByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int roleId)
        {
            try
            {
                var entity = await (from item in _databaseService.Role
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.roleId == roleId)
                                    select new RoleEntity()
                                    {
                                        roleId = item.roleId,
                                        code = item.code,
                                        name = item.name,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetRoleByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

