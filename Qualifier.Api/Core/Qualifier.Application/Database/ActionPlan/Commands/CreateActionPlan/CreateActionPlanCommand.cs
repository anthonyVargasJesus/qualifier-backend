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
                // Flutter manda las fechas sin "Z" (Kind=Unspecified) — Npgsql ahora exige UTC
                // explícito para "timestamp with time zone". Angular no tenía este problema
                // porque JSON.stringify de un Date siempre agrega "Z". Mismo fix que ya tenía
                // ActionPlanRepository.Update para el caso de editar.
                entity.startDate = DateTime.SpecifyKind(entity.startDate, DateTimeKind.Utc);
                entity.dueDate = DateTime.SpecifyKind(entity.dueDate, DateTimeKind.Utc);
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
            // No te notifiques a vos mismo por asignarte tu propia acción — mismo criterio
            // que ya usa UpdateActionPlanCommand.notifyStatusChange.
            if (entity.creationUserId != null && entity.creationUserId == entity.userId) return;

            // Proyección, no la entidad completa: UserEntity trae navigation properties
            // (standard/userState) sin configurar explícitamente en UserConfiguration, lo que
            // hace que EF Core genere shadow FKs inexistentes (ej. "StandardEntitystandardId")
            // y la consulta truene si se carga la entidad entera.
            var fcmToken = await _databaseService.User
                .Where(u => u.userId == entity.userId)
                .Select(u => u.fcmToken)
                .FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(fcmToken)) return;

            // Idem: BreachConfiguration tampoco configura sus navigation properties
            // (evaluation/breachSeverity/breachStatus/responsible/requirement/control), misma
            // proyección para evitar el mismo error de shadow FKs.
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

        private Notification createValidation(CreateActionPlanDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

