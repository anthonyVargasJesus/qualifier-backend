using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.ActionPlanStatus.Commands.CreateActionPlanStatus;
using Qualifier.Application.Firebase;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlan.Commands.CreateActionPlan

{
    public class CreateActionPlanCommand : ICreateActionPlanCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IPushNotificationService _pushNotificationService;

        public CreateActionPlanCommand(
            IDatabaseService databaseService,
            IMapper mapper,
            IPushNotificationService pushNotificationService)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _pushNotificationService = pushNotificationService;
        }

        public async Task<Object> Execute(CreateActionPlanDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<ActionPlanEntity>(model);
                const int STATUS_PENDING = 1;
                entity.actionPlanStatusId = STATUS_PENDING;
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                await _databaseService.ActionPlan.AddAsync(entity);
                await _databaseService.SaveAsync();

                await notifyAssignedUser(entity);

                return _mapper.Map<CreateActionPlanDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        // Push notification al usuario asignado — best-effort, no debe romper la creación
        // del plan de acción si el usuario no tiene token registrado o el envío falla.
        private async Task notifyAssignedUser(ActionPlanEntity entity)
        {
            if (entity.userId == null || entity.userId <= 0) return;

            var user = await _databaseService.User.FirstOrDefaultAsync(u => u.userId == entity.userId);
            if (user == null || string.IsNullOrWhiteSpace(user.fcmToken)) return;

            var breach = await _databaseService.Breach.FirstOrDefaultAsync(b => b.breachId == entity.breachId);
            var priority = await _databaseService.ActionPlanPriority
                .FirstOrDefaultAsync(p => p.actionPlanPriorityId == entity.actionPlanPriorityId);

            var breachCode = breach?.numerationToShow ?? entity.breachId.ToString();
            var priorityName = priority?.name ?? "";

            const string title = "Nueva acción de cumplimiento asignada";
            var body = $"Control {breachCode}: \"{entity.title}\" — Vence el {entity.dueDate:dd/MM/yyyy}."
                + (string.IsNullOrEmpty(priorityName) ? "" : $" Prioridad {priorityName}.");

            await _pushNotificationService.SendAsync(
                user.fcmToken!,
                title,
                body,
                new Dictionary<string, string>
                {
                    { "type", "action_plan_assigned" },
                    { "actionPlanId", entity.actionPlanId.ToString() },
                    { "breachId", entity.breachId.ToString() },
                });
        }

        private Notification createValidation(CreateActionPlanDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

