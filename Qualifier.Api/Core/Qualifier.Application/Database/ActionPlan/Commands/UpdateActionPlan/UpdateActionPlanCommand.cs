using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.ActionPlanPriority.Commands.UpdateActionPlanPriority;
using Qualifier.Application.Firebase;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ActionPlan.Commands.UpdateActionPlan
{
    public class UpdateActionPlanCommand : IUpdateActionPlanCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IActionPlanRepository _actionPlanRepository;
        private readonly IPushNotificationService _pushNotificationService;

        public UpdateActionPlanCommand(
            IDatabaseService databaseService,
            IMapper mapper,
            IActionPlanRepository actionPlanRepository,
            IPushNotificationService pushNotificationService)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _actionPlanRepository = actionPlanRepository;
            _pushNotificationService = pushNotificationService;
        }

        public async Task<Object> Execute(UpdateActionPlanDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                // Estado, responsable y creador ANTES de actualizar, para saber si el estado o
                // el responsable realmente cambiaron y a quién avisarle.
                var before = await _databaseService.ActionPlan
                    .Where(a => a.actionPlanId == id)
                    .Select(a => new { a.actionPlanStatusId, a.creationUserId, a.title, a.userId })
                    .FirstOrDefaultAsync();

                await _actionPlanRepository.Update(id, _mapper.Map<ActionPlanEntity>(model));

                // Proyección (no la entidad completa, por el shadow FK de siempre) Y AsNoTracking:
                // sin esto, esta segunda consulta choca con la entidad que el repositorio ya dejó
                // trackeada como Modified al hacer el Update de arriba ("already being tracked").
                var updatedEntity = await _databaseService.ActionPlan
                    .AsNoTracking()
                    .Where(item => item.actionPlanId == id && !(item.isDeleted ?? false))
                    .Select(a => new ActionPlanEntity
                    {
                        actionPlanId = a.actionPlanId,
                        breachId = a.breachId,
                        evaluationId = a.evaluationId,
                        standardId = a.standardId,
                        title = a.title,
                        description = a.description,
                        userId = a.userId,
                        startDate = a.startDate,
                        dueDate = a.dueDate,
                        actionPlanStatusId = a.actionPlanStatusId,
                        actionPlanPriorityId = a.actionPlanPriorityId,
                        companyId = a.companyId,
                    })
                    .FirstOrDefaultAsync();

                if (before != null && model.actionPlanStatusId != null && before.actionPlanStatusId != model.actionPlanStatusId)
                {
                    await notifyStatusChange(
                        before.creationUserId,
                        before.title,
                        model.actionPlanStatusId.Value,
                        model.updateUserId,
                        id,
                        model.breachId,
                        updatedEntity?.companyId);
                }

                if (before != null && model.userId != null && model.userId > 0 && before.userId != model.userId)
                {
                    await notifyReassignedUser(updatedEntity, model.updateUserId);
                }

                return _mapper.Map<UpdateActionPlanDto>(updatedEntity);
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        // Le avisa a quien CREÓ el plan de acción (típicamente el responsable del control) que
        // el usuario asignado cambió el estado — best-effort, no debe romper el guardado.
        private async Task notifyStatusChange(
            int? creatorUserId,
            string actionPlanTitle,
            int newStatusId,
            int? changedByUserId,
            int actionPlanId,
            int? breachId,
            int? companyId)
        {
            if (creatorUserId == null || creatorUserId <= 0) return;
            if (changedByUserId != null && changedByUserId == creatorUserId) return;

            var fcmToken = await _databaseService.User
                .Where(u => u.userId == creatorUserId)
                .Select(u => u.fcmToken)
                .FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(fcmToken)) return;

            var statusName = await _databaseService.ActionPlanStatus
                .Where(s => s.actionPlanStatusId == newStatusId)
                .Select(s => s.name)
                .FirstOrDefaultAsync() ?? "";

            const string title = "Actualización en tu plan de acción";
            var body = string.IsNullOrEmpty(statusName)
                ? $"\"{actionPlanTitle}\" cambió de estado."
                : $"\"{actionPlanTitle}\" cambió de estado a \"{statusName}\".";

            await _pushNotificationService.SendAsync(
                creatorUserId.Value,
                fcmToken!,
                title,
                body,
                "action_plan_status_changed",
                actionPlanId: actionPlanId,
                breachId: breachId,
                companyId: companyId,
                data: new Dictionary<string, string>
                {
                    { "type", "action_plan_status_changed" },
                    { "actionPlanId", actionPlanId.ToString() },
                });
        }

        // Le avisa al nuevo responsable que se le asignó esta acción — best-effort, no debe
        // romper el guardado. Mismo criterio/texto que CreateActionPlanCommand.notifyAssignedUser
        // (esa solo cubre la creación; reasignar el responsable de una acción ya existente no
        // pasaba por ningún lado que notificara a nadie).
        private async Task notifyReassignedUser(ActionPlanEntity? entity, int? updatedByUserId)
        {
            if (entity == null || entity.userId == null || entity.userId <= 0) return;
            // No te notifiques a vos mismo por reasignarte una acción — mismo criterio que
            // notifyAssignedUser en la creación.
            if (updatedByUserId != null && updatedByUserId == entity.userId) return;

            var fcmToken = await _databaseService.User
                .Where(u => u.userId == entity.userId)
                .Select(u => u.fcmToken)
                .FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(fcmToken)) return;

            var breachNumerationToShow = await _databaseService.Breach
                .Where(b => b.breachId == entity.breachId)
                .Select(b => b.numerationToShow)
                .FirstOrDefaultAsync();
            var priority = await _databaseService.ActionPlanPriority
                .FirstOrDefaultAsync(p => p.actionPlanPriorityId == entity.actionPlanPriorityId);

            var breachCode = breachNumerationToShow ?? entity.breachId.ToString();
            var priorityName = priority?.name ?? "";

            const string title = "Nueva acción de cumplimiento asignada";
            var body = $"Control {breachCode}: \"{entity.title}\" — Vence el {entity.dueDate:dd/MM/yyyy}."
                + (string.IsNullOrEmpty(priorityName) ? "" : $" Prioridad {priorityName}.");

            await _pushNotificationService.SendAsync(
                entity.userId.Value,
                fcmToken!,
                title,
                body,
                "action_plan_assigned",
                actionPlanId: entity.actionPlanId,
                breachId: entity.breachId,
                companyId: entity.companyId,
                data: new Dictionary<string, string>
                {
                    { "type", "action_plan_assigned" },
                    { "actionPlanId", entity.actionPlanId.ToString() },
                    { "breachId", entity.breachId.ToString() },
                });
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.ActionPlan.CountAsync(item => item.actionPlanId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateActionPlanDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}
