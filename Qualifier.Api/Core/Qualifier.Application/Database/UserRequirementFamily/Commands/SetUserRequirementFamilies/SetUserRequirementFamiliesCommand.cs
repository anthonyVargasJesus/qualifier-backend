using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UserRequirementFamily.Commands.SetUserRequirementFamilies
{
    // Reemplaza TODAS las familias de cláusula asignadas a un usuario por la lista que llega del
    // checklist (no es alta/baja fila por fila, es "guardar la selección completa").
    public class SetUserRequirementFamiliesCommand : ISetUserRequirementFamiliesCommand
    {
        private readonly IDatabaseService _databaseService;

        public SetUserRequirementFamiliesCommand(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(SetUserRequirementFamiliesDto model)
        {
            try
            {
                Notification notification = new Notification();
                model.requiredFieldsValidation(notification);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var existentes = await _databaseService.UserRequirementFamily
                    .Where(x => x.userId == model.userId && x.standardId == model.standardId)
                    .ToListAsync();

                _databaseService.UserRequirementFamily.RemoveRange(existentes);

                foreach (var requirementId in model.requirementIds.Distinct())
                {
                    await _databaseService.UserRequirementFamily.AddAsync(new UserRequirementFamilyEntity
                    {
                        userId = model.userId,
                        requirementId = requirementId,
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
