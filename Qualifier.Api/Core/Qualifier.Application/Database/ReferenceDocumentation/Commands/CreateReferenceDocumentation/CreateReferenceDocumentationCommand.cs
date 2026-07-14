using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Firebase;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ReferenceDocumentation.Commands.CreateReferenceDocumentation

{
    public class CreateReferenceDocumentationCommand : ICreateReferenceDocumentationCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IPushNotificationService _pushNotificationService;

        public CreateReferenceDocumentationCommand(
            IDatabaseService databaseService,
            IMapper mapper,
            IPushNotificationService pushNotificationService)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _pushNotificationService = pushNotificationService;
        }

        public async Task<Object> Execute(CreateReferenceDocumentationDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<ReferenceDocumentationEntity>(model);
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                await _databaseService.ReferenceDocumentation.AddAsync(entity);
                await _databaseService.SaveAsync();

                await notifyActionPlanAssigneesOfEvidence(model);

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        // Le avisa a quien está ASIGNADO (userId, no creationUserId) al/los plan(es) de acción
        // del control o requisito evaluado que se agregó una evidencia nueva — es quien tiene
        // que ejecutar la acción correctiva y a quien le interesa saber que hay evidencia nueva,
        // no necesariamente quien creó la tarea. Best-effort, no debe romper el guardado.
        private async Task notifyActionPlanAssigneesOfEvidence(CreateReferenceDocumentationDto model)
        {
            int? controlId = null;
            int? requirementId = null;

            if (model.controlEvaluationId != null)
            {
                controlId = await _databaseService.ControlEvaluation
                    .Where(c => c.controlEvaluationId == model.controlEvaluationId)
                    .Select(c => (int?)c.controlId)
                    .FirstOrDefaultAsync();
            }
            else if (model.requirementEvaluationId != null)
            {
                requirementId = await _databaseService.RequirementEvaluation
                    .Where(r => r.requirementEvaluationId == model.requirementEvaluationId)
                    .Select(r => (int?)r.requirementId)
                    .FirstOrDefaultAsync();
            }

            if (controlId == null && requirementId == null) return;

            var actionPlans = await _databaseService.Breach
                .Where(b => b.evaluationId == model.evaluationId
                    && (controlId != null ? b.controlId == controlId : b.requirementId == requirementId))
                .Join(_databaseService.ActionPlan, b => b.breachId, a => a.breachId, (b, a) => new
                {
                    a.actionPlanId,
                    a.userId,
                    a.title,
                    a.breachId,
                })
                .ToListAsync();

            foreach (var assigneeGroup in actionPlans
                .Where(a => a.userId != null && a.userId != model.creationUserId)
                .GroupBy(a => a.userId))
            {
                var fcmToken = await _databaseService.User
                    .Where(u => u.userId == assigneeGroup.Key)
                    .Select(u => u.fcmToken)
                    .FirstOrDefaultAsync();
                if (string.IsNullOrWhiteSpace(fcmToken)) continue;

                var first = assigneeGroup.First();
                const string title = "Nueva evidencia en un plan de acción";
                var body = $"Se agregó una evidencia (\"{model.name}\") en \"{first.title}\".";

                await _pushNotificationService.SendAsync(
                    assigneeGroup.Key!.Value,
                    fcmToken!,
                    title,
                    body,
                    "action_plan_evidence_added",
                    actionPlanId: first.actionPlanId,
                    breachId: first.breachId,
                    companyId: model.companyId,
                    data: new Dictionary<string, string>
                    {
                        { "type", "action_plan_evidence_added" },
                        { "actionPlanId", first.actionPlanId.ToString() },
                    });
            }
        }

        private Notification createValidation(CreateReferenceDocumentationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}
