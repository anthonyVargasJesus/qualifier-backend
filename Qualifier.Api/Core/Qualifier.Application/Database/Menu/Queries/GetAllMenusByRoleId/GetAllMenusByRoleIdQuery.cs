using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Menu.Queries.GetMenusByCompanyId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Menu.Queries.GetAllMenusByRoleId
{
    public class GetAllMenusByRoleIdQuery : IGetAllMenusByRoleIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetAllMenusByRoleIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int roleId, int companyId)
        {
            try
            {
                var menus = await (from menu in _databaseService.Menu
                                      where ((menu.isDeleted == null || menu.isDeleted == false) && menu.companyId == companyId)
                                      select new MenuEntity
                                      {
                                          menuId = menu.menuId,
                                          name = menu.name,
                                          image = menu.image,
                                          companyId = menu.companyId,
                                      })
                .ToListAsync();

                var optionInMenus = await (from optionInMenu in _databaseService.OptionInMenu
                                             join option in _databaseService.Option on optionInMenu.option equals option
                                             where ((optionInMenu.isDeleted == null || optionInMenu.isDeleted == false) && optionInMenu.companyId == companyId)
                                             select new OptionInMenuEntity
                                             {
                                                 optionInMenuId = optionInMenu.optionInMenuId,
                                                 menuId = optionInMenu.menuId,
                                                 optionId = optionInMenu.optionId,
                                                 order = optionInMenu.order,
                                                 companyId = optionInMenu.companyId,
                                                 option = new OptionEntity
                                                 {
                                                     optionId = optionInMenu.optionId,
                                                     name = option.name,
                                                     order = optionInMenu.order,
                                                 },
                                             })
                                      .OrderBy(option => option.order)
                .ToListAsync();

                foreach (var menu in menus)
                {
                    List<OptionEntity> options = new List<OptionEntity>();
                    foreach (var optionInMenu in optionInMenus)
                        if (menu.menuId == optionInMenu.menuId)
                            options.Add(optionInMenu.option);
   
                    menu.options = options.OrderBy(option => option.order).ToList(); ;
                }

                var optionInMenuInRoles = await (from optionInMenuInRole in _databaseService.OptionInMenuInRole
                                                                                           where ((optionInMenuInRole.isDeleted == null || optionInMenuInRole.isDeleted == false) && optionInMenuInRole.companyId == companyId)
                                                                                           select new OptionInMenuInRoleEntity
                                                                                           {
                                                                                               optionInMenuInRoleId = optionInMenuInRole.optionInMenuInRoleId,
                                                                                               optionId = optionInMenuInRole.optionId,
                                                                                               order = optionInMenuInRole.order,
                                                                                               menuId = optionInMenuInRole.menuId,
                                                                                               roleId = optionInMenuInRole.roleId,
                                                                                           }).ToListAsync();
                foreach (MenuEntity menu in menus)
                {
                    List<OptionEntity> options = new List<OptionEntity>();
                    foreach (OptionInMenuEntity optionInMenu in optionInMenus)
                    {
                        if (menu.menuId == optionInMenu.menuId)
                        {

                            int counter = 0;
                            foreach (OptionInMenuInRoleEntity item in optionInMenuInRoles)
                            {
                                if (item.optionId == optionInMenu.optionId &&
                                    item.menuId == menu.menuId &&
                                    item.roleId == roleId)
                                {
                                    optionInMenu.option.optionInMenuInRoleId = item.optionInMenuInRoleId;
                                    counter++;
                                }
                            }

                            if (counter > 0)
                                optionInMenu.option.isChecked = true;

                            options.Add(optionInMenu.option);
                        }
                    }
                    menu.options = options;
                }

                BaseResponseDto<GetAllMenusByRoleIdDto> baseResponseDto = new BaseResponseDto<GetAllMenusByRoleIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllMenusByRoleIdDto>>(menus);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }


    }
}
