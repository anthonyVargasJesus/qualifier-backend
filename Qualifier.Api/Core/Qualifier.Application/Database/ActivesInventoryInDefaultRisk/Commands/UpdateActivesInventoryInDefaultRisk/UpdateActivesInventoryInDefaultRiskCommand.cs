using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.UpdateActivesInventoryInDefaultRisk
{
public class UpdateActivesInventoryInDefaultRiskCommand : IUpdateActivesInventoryInDefaultRiskCommand
{
private readonly IDatabaseService _databaseService;
private readonly IMapper _mapper;
private readonly IActivesInventoryInDefaultRiskRepository _activesInventoryInDefaultRiskRepository;

public UpdateActivesInventoryInDefaultRiskCommand(IDatabaseService databaseService, IMapper mapper, IActivesInventoryInDefaultRiskRepository activesInventoryInDefaultRiskRepository)
{
_databaseService = databaseService;
_mapper = mapper;
_activesInventoryInDefaultRiskRepository = activesInventoryInDefaultRiskRepository;
}

public async Task<Object> Execute(UpdateActivesInventoryInDefaultRiskDto model, int id)
{
try
{
Notification notification = this.updateValidation(model);
if (notification.hasErrors())
return BaseApplication.getApplicationErrorResponse(notification.errors);

Notification existsNotification = await this.existsValidationAsync(id);
if (existsNotification.hasErrors())
return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

await _activesInventoryInDefaultRiskRepository.Update(id, _mapper.Map<ActivesInventoryInDefaultRiskEntity>(model));

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
int total = await _databaseService.ActivesInventoryInDefaultRisk.CountAsync(item => item.activesInventoryInDefaultRiskId == id);
if (total == 0)
notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
return notification;
}

private Notification updateValidation(UpdateActivesInventoryInDefaultRiskDto request)
{
Notification notification = new Notification();
request.requiredFieldsValidation(notification);
return notification;
}

}
}

