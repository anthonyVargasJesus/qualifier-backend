using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UserControlGroup.Commands.SetUserControlGroups
{
    // Reemplaza TODAS las asignaciones de grupos de control de un usuario por la lista que llega
    // del checklist (no es alta/baja fila por fila, es "guardar la selección completa").
    public class SetUserControlGroupsCommand : ISetUserControlGroupsCommand
    {
        private readonly IDatabaseService _databaseService;

        public SetUserControlGroupsCommand(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(SetUserControlGroupsDto model)
        {
            try
            {
                Notification notification = new Notification();
                model.requiredFieldsValidation(notification);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var existentes = await _databaseService.UserControlGroup
                    .Where(x => x.userId == model.userId && x.standardId == model.standardId)
                    .ToListAsync();

                _databaseService.UserControlGroup.RemoveRange(existentes);

                foreach (var controlGroupId in model.controlGroupIds.Distinct())
                {
                    await _databaseService.UserControlGroup.AddAsync(new UserControlGroupEntity
                    {
                        userId = model.userId,
                        controlGroupId = controlGroupId,
                        standardId = model.standardId,
                        companyId = model.companyId,
                        creationUserId = model.creationUserId,
                        creationDate = DateTime.UtcNow,
                    });
                }

                await _databaseService.SaveAsync();
                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
