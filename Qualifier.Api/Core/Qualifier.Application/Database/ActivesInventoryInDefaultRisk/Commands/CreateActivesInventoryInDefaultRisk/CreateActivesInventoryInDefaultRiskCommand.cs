using AutoMapper;
using Qualifier.Common.Application.NotificationPattern; 
using Qualifier.Common.Application.Service;
 using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.CreateActivesInventoryInDefaultRisk

{
public class CreateActivesInventoryInDefaultRiskCommand : ICreateActivesInventoryInDefaultRiskCommand
{

private readonly IDatabaseService _databaseService;
private readonly IMapper _mapper;

public CreateActivesInventoryInDefaultRiskCommand(IDatabaseService databaseService, IMapper mapper)
{
_databaseService = databaseService;
_mapper = mapper;
}

public async Task<Object> Execute(CreateActivesInventoryInDefaultRiskDto model)
{
try
{
Notification notification = this.createValidation(model);
if (notification.hasErrors())
return BaseApplication.getApplicationErrorResponse(notification.errors);

var entity = _mapper.Map<ActivesInventoryInDefaultRiskEntity>(model);
entity.creationDate = DateTime.UtcNow;
entity.creationUserId = model.creationUserId;
await _databaseService.ActivesInventoryInDefaultRisk.AddAsync(entity);
await _databaseService.SaveAsync();
return model;
}
catch (Exception)
{
return BaseApplication.getExceptionErrorResponse();
}
}

private Notification createValidation(CreateActivesInventoryInDefaultRiskDto request)
{
Notification notification = new Notification();
request.requiredFieldsValidation(notification);
return notification;
}

}
}

