using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MenuInRole.Queries.GetMenuInRoleById
{
    public class GetMenuInRoleByIdQuery : IGetMenuInRoleByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMenuInRoleByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int menuInRoleId)
        {
            try
            {
                var entity = await (from item in _databaseService.MenuInRole
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.menuInRoleId == menuInRoleId)
                                    select new MenuInRoleEntity()
                                    {
                                        menuInRoleId = item.menuInRoleId,
                                        menuId = item.menuId,
                                        roleId = item.roleId,
                                        order = item.order,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetMenuInRoleByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

