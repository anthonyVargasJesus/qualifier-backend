using AutoMapper;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.OptionInMenuInRole.Commands.CreateOptionInMenuInRole

{
    public class CreateOptionInMenuInRoleCommand : ICreateOptionInMenuInRoleCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateOptionInMenuInRoleCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(CreateOptionInMenuInRoleDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<OptionInMenuInRoleEntity>(model);
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                await _databaseService.OptionInMenuInRole.AddAsync(entity);
                await _databaseService.SaveAsync();

                List<MenuInRoleEntity> menuInRoles =   (from menuInRole in _databaseService.MenuInRole
                                                               join menu in _databaseService.Menu on menuInRole.menu equals menu
                                                               where menuInRole.roleId == model.roleId && (menuInRole.isDeleted == null || menuInRole.isDeleted == false)
                                                               select new MenuInRoleEntity()
                                                               {
                                                                   menuInRoleId = menuInRole.menuInRoleId,
                                                                   menu = new MenuEntity { menuId = menu.menuId, name = menu.name },
                                                                   order = menuInRole.order,
                                                               })
                         .OrderBy(b => b.menuInRoleId)
                         .ToList();

                if (menuInRoles.Where(e => e.menu.menuId == model.menuId).Count() == 0)
                {
                    MenuInRoleEntity menuInRole = new MenuInRoleEntity();
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    menuInRole.menuId = model.menuId;
                    menuInRole.roleId = model.roleId;
                    menuInRole.companyId = model.companyId!.Value;
                    await _databaseService.MenuInRole.AddAsync(menuInRole);
                    await _databaseService.SaveAsync();
                }

                return model;
            }
            catch (Exception)
            {
              return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateOptionInMenuInRoleDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

