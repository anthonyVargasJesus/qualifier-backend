using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelsByCompanyId;
using Qualifier.Common.Application.Dto;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.User.Queries.GetMenus
{
    public class GetMenusQuery : IGetMenusQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMenusQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int userId)
        {
            try
            {
                LoginEntity login = new LoginEntity();

                login.roles = await (from roleInUser in _databaseService.RoleInUser
                               join role in _databaseService.Role on roleInUser.role equals role
                               where roleInUser.userId == userId && (roleInUser.isDeleted == null || roleInUser.isDeleted == false)
                               select new RoleEntity
                               {
                                   roleId = role.roleId,
                                   name = role.name,
                                   code = role.code
                               }).ToListAsync();

                asignMenus(login.roles);
                login.setMenus(login.roles);


                if (login.menus != null)
                    login.menus = login.menus.OrderBy(x => x.order).ToList();

                BaseResponseDto<GetMenusMenuQueryDto> baseResponseDto = new BaseResponseDto<GetMenusMenuQueryDto>();
                baseResponseDto.data = _mapper.Map<List<GetMenusMenuQueryDto>>(login.menus);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        private void asignMenus(List<RoleEntity> roles)
        {
            foreach (RoleEntity role in roles)
            {
                role.menus = (from menuInRole in _databaseService.MenuInRole
                              join menu in _databaseService.Menu on menuInRole.menu equals menu
                              where menuInRole.roleId == role.roleId && (menuInRole.isDeleted == null || menuInRole.isDeleted == false)
                              select new MenuEntity
                              {
                                  menuId = menu.menuId,
                                  name = menu.name,
                                  image = menu.image,
                                  order = menuInRole.order,
                              }).ToList();

                asignOptions(role.menus, role.roleId);
            }
        }

        private void asignOptions(List<MenuEntity> menus, int roleId)
        {
            foreach (MenuEntity menu in menus)
                menu.options = (from optionInMenuInRole in _databaseService.OptionInMenuInRole
                                join option in _databaseService.Option on optionInMenuInRole.option equals option
                                where optionInMenuInRole.menuId == menu.menuId && optionInMenuInRole.roleId == roleId && (optionInMenuInRole.isDeleted == null || optionInMenuInRole.isDeleted == false)
                                select new OptionEntity
                                {
                                    optionId = option.optionId,
                                    name = option.name,
                                    url = option.url
                                }).ToList();
        }


    }
}
