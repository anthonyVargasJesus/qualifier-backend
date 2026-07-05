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

                await notifyActionPlanCreatorsOfEvidence(model);

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        // Le avisa a quien CREÓ el/los plan(es) de acción del control o requisito evaluado que
        // se agregó una evidencia nueva — best-effort, no debe romper el guardado de la evidencia.
        private async Task notifyActionPlanCreatorsOfEvidence(CreateReferenceDocumentationDto model)
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
                    a.creationUserId,
                    a.title,
                    a.breachId,
                })
                .ToListAsync();

            foreach (var creatorGroup in actionPlans
                .Where(a => a.creationUserId != null && a.creationUserId != model.creationUserId)
                .GroupBy(a => a.creationUserId))
            {
                var fcmToken = await _databaseService.User
                    .Where(u => u.userId == creatorGroup.Key)
                    .Select(u => u.fcmToken)
                    .FirstOrDefaultAsync();
                if (string.IsNullOrWhiteSpace(fcmToken)) continue;

                var first = creatorGroup.First();
                const string title = "Nueva evidencia en un plan de acción";
                var body = $"Se agregó una evidencia (\"{model.name}\") en \"{first.title}\".";

                await _pushNotificationService.SendAsync(
                    creatorGroup.Key!.Value,
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
