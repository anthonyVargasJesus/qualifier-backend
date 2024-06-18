using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;namespace Qualifier.Application.Database.MenuInRole.Commands.UpdateMenuInRole
{
public class UpdateMenuInRoleCommand : IUpdateMenuInRoleCommand
{
private readonly IDatabaseService _databaseService;
private readonly IMapper _mapper;
private readonly IMenuInRoleRepository _menuInRoleRepository;

public UpdateMenuInRoleCommand(IDatabaseService databaseService, IMapper mapper, IMenuInRoleRepository menuInRoleRepository)
{
_databaseService = databaseService;
_mapper = mapper;
_menuInRoleRepository = menuInRoleRepository;
}

public async Task<Object> Execute(UpdateMenuInRoleDto model, int id)
{
try
{
Notification notification = this.updateValidation(model);
if (notification.hasErrors())
return BaseApplication.getApplicationErrorResponse(notification.errors);

Notification existsNotification = await this.existsValidationAsync(id);
if (existsNotification.hasErrors())
return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

await _menuInRoleRepository.Update(id, _mapper.Map<MenuInRoleEntity>(model));

return model;
}
catch (Exception)
{
return BaseApplication.getExceptionErrorResponse();
}
}

private async Task<Notification> existsValidationAsync(int id)
{
Notification notification = new Notification();
int total = await _databaseService.MenuInRole.CountAsync(item => item.menuInRoleId == id);
if (total == 0)
notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
return notification;
}

private Notification updateValidation(UpdateMenuInRoleDto request)
{
Notification notification = new Notification();
request.requiredFieldsValidation(notification);
return notification;
}

}
}

