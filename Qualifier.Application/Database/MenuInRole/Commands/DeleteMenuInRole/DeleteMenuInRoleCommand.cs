using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Common.Application.Dto;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.MenuInRole.Commands.DeleteMenuInRole
{
public class DeleteMenuInRoleCommand : IDeleteMenuInRoleCommand
{
private readonly IDatabaseService _databaseService;
private readonly IMenuInRoleRepository _menuInRoleRepository;

public DeleteMenuInRoleCommand(IDatabaseService databaseService, IMenuInRoleRepository menuInRoleRepository)
{
_databaseService = databaseService;
_menuInRoleRepository = menuInRoleRepository;
}

public async Task<Object> Execute(int id, int updateUserId)
{
try
{
Notification existsNotification = await this.existsValidationAsync(id);
if (existsNotification.hasErrors())
return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

await _menuInRoleRepository.Delete(id, updateUserId);

BaseResponseCommandDto baseResponseCommandDto = new BaseResponseCommandDto();
baseResponseCommandDto.response = "¡Registro eliminado!";
return baseResponseCommandDto;

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

}
}

