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

                await notifyActionPlanParticipantsOfEvidence(model);

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private const string EvidenceAddedNotificationType = "action_plan_evidence_added";
        private const string EvidenceAddedNotificationTitle = "Nueva evidencia en un plan de acción";

        // Los dos lados de un plan de acción que pueden querer enterarse de una evidencia nueva:
        // el ASIGNADO (userId — quien ejecuta la acción correctiva) y quien CREÓ el plan
        // (creationUserId — normalmente el dueño del control/requisito que lo creó y se lo
        // asignó a otra persona).
        private record ActionPlanEvidenceTarget(int actionPlanId, int? userId, int? creationUserId, string title, int breachId);
        private record EvidenceRecipient(int userId, int actionPlanId, string title, int breachId);

        // Le avisa a los dos lados del plan de acción cuando se agrega una evidencia nueva.
        // Cualquiera de los dos puede ser quien sube la evidencia, así que a ese se lo excluye
        // para no autonotificarlo; al otro sí le interesa enterarse. Best-effort, no debe romper
        // el guardado de la evidencia.
        private async Task notifyActionPlanParticipantsOfEvidence(CreateReferenceDocumentationDto model)
        {
            var (controlId, requirementId) = await resolveEvaluatedItemAsync(model);
            if (controlId == null && requirementId == null) return;

            var actionPlans = await getActionPlanTargetsAsync(model.evaluationId, controlId, requirementId);

            var recipients = buildNotificationRecipients(actionPlans, model.creationUserId);
            if (recipients.Count == 0) return;

            await sendEvidenceNotificationsAsync(recipients, model);
        }

        private async Task<(int? controlId, int? requirementId)> resolveEvaluatedItemAsync(CreateReferenceDocumentationDto model)
        {
            if (model.controlEvaluationId != null)
            {
                var controlId = await _databaseService.ControlEvaluation
                    .Where(c => c.controlEvaluationId == model.controlEvaluationId)
                    .Select(c => (int?)c.controlId)
                    .FirstOrDefaultAsync();
                return (controlId, null);
            }

            if (model.requirementEvaluationId != null)
            {
                var requirementId = await _databaseService.RequirementEvaluation
                    .Where(r => r.requirementEvaluationId == model.requirementEvaluationId)
                    .Select(r => (int?)r.requirementId)
                    .FirstOrDefaultAsync();
                return (null, requirementId);
            }

            return (null, null);
        }

        private async Task<List<ActionPlanEvidenceTarget>> getActionPlanTargetsAsync(int? evaluationId, int? controlId, int? requirementId)
        {
            return await _databaseService.Breach
                .Where(b => b.evaluationId == evaluationId
                    && (controlId != null ? b.controlId == controlId : b.requirementId == requirementId))
                .Join(_databaseService.ActionPlan, b => b.breachId, a => a.breachId, (b, a) =>
                    new ActionPlanEvidenceTarget(a.actionPlanId, a.userId, a.creationUserId, a.title, a.breachId))
                .ToListAsync();
        }

        // Colapsa asignado+creador en una lista de destinatarios únicos, sin quien subió la
        // evidencia — pura (sin acceso a datos), fácil de testear.
        private static List<EvidenceRecipient> buildNotificationRecipients(List<ActionPlanEvidenceTarget> actionPlans, int? actorUserId)
        {
            return actionPlans
                .SelectMany(a => new[] { a.userId, a.creationUserId },
                    (a, userId) => new { userId, a.actionPlanId, a.title, a.breachId })
                .Where(r => r.userId != null && r.userId != actorUserId)
                .GroupBy(r => r.userId!.Value)
                .Select(g => new EvidenceRecipient(g.Key, g.First().actionPlanId, g.First().title, g.First().breachId))
                .ToList();
        }

        private async Task sendEvidenceNotificationsAsync(List<EvidenceRecipient> recipients, CreateReferenceDocumentationDto model)
        {
            var recipientIds = recipients.Select(r => r.userId).ToList();
            var tokensByUserId = await _databaseService.User
                .Where(u => recipientIds.Contains(u.userId))
                .ToDictionaryAsync(u => u.userId, u => u.fcmToken);

            foreach (var recipient in recipients)
            {
                if (!tokensByUserId.TryGetValue(recipient.userId, out var fcmToken) || string.IsNullOrWhiteSpace(fcmToken))
                    continue;

                var body = $"Se agregó una evidencia (\"{model.name}\") en \"{recipient.title}\".";

                await _pushNotificationService.SendAsync(
                    recipient.userId,
                    fcmToken!,
                    EvidenceAddedNotificationTitle,
                    body,
                    EvidenceAddedNotificationType,
                    actionPlanId: recipient.actionPlanId,
                    breachId: recipient.breachId,
                    companyId: model.companyId,
                    data: new Dictionary<string, string>
                    {
                        { "type", EvidenceAddedNotificationType },
                        { "actionPlanId", recipient.actionPlanId.ToString() },
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
