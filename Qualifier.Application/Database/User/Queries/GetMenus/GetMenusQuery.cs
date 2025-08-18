using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
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


                var user = await (from item in _databaseService.User
                                  join standard in _databaseService.Standard on item.standard equals standard
                                  where ((item.isDeleted == null || item.isDeleted == false) && item.userId == userId)
                                  select new UserEntity()
                                  {
                                      standardId = item.standardId,
                                      standard = new StandardEntity
                                      {
                                          name = standard.name,
                                      },
                                  }).FirstOrDefaultAsync();

                var standardName = "";
                if (user != null)
                    if (user.standard != null)
                        standardName = user.standard.name;

                asignMenus(login.roles, user.standardId, standardName);
                login.setMenus(login.roles);

                if (login.menus != null)
                    login.menus = login.menus.OrderBy(x => x.order).ToList();

                BaseResponseDto<GetMenusMenuQueryDto> baseResponseDto = new BaseResponseDto<GetMenusMenuQueryDto>();
                baseResponseDto.data = _mapper.Map<List<GetMenusMenuQueryDto>>(login.menus);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private void asignMenus(List<RoleEntity> roles, int standardId, string standardName)
        {
            const string CURRENT_STANDARD_MENU = "CURRENT_STANDARD";

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



                asignOptions(role.menus, role.roleId, standardId, standardName);
                foreach (var menu in role.menus)
                    if (menu.name == CURRENT_STANDARD_MENU)
                    {
                        menu.name = standardName;
                    }
            }
        }

        private void asignOptions(List<MenuEntity> menus, int roleId, int standardId, string standardName)
        {
            const string CURRENT_STANDARD_MENU = "CURRENT_STANDARD";
            const string CURRENT_STANDARD_ROUTE = "security/edit-standard";

            foreach (MenuEntity menu in menus)
            {
                var options = (from optionInMenuInRole in _databaseService.OptionInMenuInRole
                               join option in _databaseService.Option on optionInMenuInRole.option equals option
                               where optionInMenuInRole.menuId == menu.menuId && optionInMenuInRole.roleId == roleId && (optionInMenuInRole.isDeleted == null || optionInMenuInRole.isDeleted == false)
                               select new OptionEntity
                               {
                                   optionId = option.optionId,
                                   name = option.name,
                                   url = option.url,
                                   order = optionInMenuInRole.order,
                               }).ToList();


                //foreach (var option in options)
                //    if (option.url == CURRENT_STANDARD_ROUTE)
                //    {
                //        const string STANDARDS_ROUTE = "current-standard/standard";
                //        if (option.url != STANDARDS_ROUTE)
                //            option.url = option.url + "/" + standardId + "/1";

                //        option.name = standardName;
                //    }

                if (menu.name == CURRENT_STANDARD_MENU)
                {
                    foreach (var option in options)
                    {
                        const string STANDARDS_ROUTE = "current-standard/standard";
                        const string DOCUMENT_TYPE_ROUTE = "current-standard/document-type";
                        const string CONTROL_TYPE_ROUTE = "current-standard/control-type";
                        if (option.url != STANDARDS_ROUTE && option.url != DOCUMENT_TYPE_ROUTE && option.url != CONTROL_TYPE_ROUTE)
                            option.url = option.url + "/" + standardId;
                        //option.name = standardName;
                    }


                }

                menu.options = options.OrderBy(e => e.order).ToList();

            }


        }



    }
}
