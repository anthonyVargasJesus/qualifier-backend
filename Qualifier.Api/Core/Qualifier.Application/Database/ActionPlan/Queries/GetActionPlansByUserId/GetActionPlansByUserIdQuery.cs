using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByUserId
{
    // Tareas asignadas a un usuario real (userId), sin importar en qué brecha/control estén —
    // es la vista "Mis acciones" del responsable, no la vista por brecha del dueño del control.
    public class GetActionPlansByUserIdQuery : IGetActionPlansByUserIdQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetActionPlansByUserIdQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int userId, int evaluationId)
        {
            try
            {
                var raw = await (from ap in _databaseService.ActionPlan
                                 join status in _databaseService.ActionPlanStatus on ap.actionPlanStatusId equals status.actionPlanStatusId
                                 join priority in _databaseService.ActionPlanPriority on ap.actionPlanPriorityId equals priority.actionPlanPriorityId
                                 join breach in _databaseService.Breach on ap.breachId equals breach.breachId
                                 where (ap.isDeleted == null || ap.isDeleted == false)
                                       && ap.userId == userId && ap.evaluationId == evaluationId
                                       // Una vez completada, deja de ser una tarea pendiente del
                                       // responsable — si el dueño del requisito la reabre o
                                       // reasigna, vuelve a aparecer porque cambia el estado.
                                       && status.abbreviation != "COMP" && status.abbreviation != "CERR"
                                 orderby ap.dueDate
                                 select new
                                 {
                                     ap.actionPlanId,
                                     ap.breachId,
                                     ap.evaluationId,
                                     ap.standardId,
                                     userId = ap.userId ?? 0,
                                     breachNumerationToShow = breach.numerationToShow ?? "",
                                     breachTitle = breach.title,
                                     ap.title,
                                     ap.description,
                                     ap.startDate,
                                     ap.dueDate,
                                     ap.actionPlanStatusId,
                                     statusName = status.name,
                                     statusColor = status.color,
                                     ap.actionPlanPriorityId,
                                     priorityName = priority.name,
                                     priorityColor = priority.color,
                                     breachType = breach.type,
                                     breachRequirementId = breach.requirementId,
                                     breachControlId = breach.controlId,
                                 }).ToListAsync();

                // MAE_BREACH solo guarda requirementId/controlId (no el id de la evaluación
                // puntual) — se resuelve acá para que el frontend pueda subir evidencias contra
                // la MISMA evaluación que ve el dueño del control en gap-panel.
                var reqIds = raw.Where(r => r.breachType == "1").Select(r => r.breachRequirementId).Distinct().ToList();
                var ctrlIds = raw.Where(r => r.breachType == "2").Select(r => r.breachControlId).Distinct().ToList();

                var reqEvaluations = await _databaseService.RequirementEvaluation
                    .Where(re => re.evaluationId == evaluationId && reqIds.Contains(re.requirementId))
                    .Select(re => new { re.requirementId, re.requirementEvaluationId })
                    .ToListAsync();

                var ctrlEvaluations = await _databaseService.ControlEvaluation
                    .Where(ce => ce.evaluationId == evaluationId && ctrlIds.Contains(ce.controlId))
                    .Select(ce => new { ce.controlId, ce.controlEvaluationId })
                    .ToListAsync();

                var data = raw.Select(r => new GetActionPlansByUserIdDto
                {
                    actionPlanId = r.actionPlanId,
                    breachId = r.breachId,
                    evaluationId = r.evaluationId,
                    standardId = r.standardId,
                    userId = r.userId,
                    breachNumerationToShow = r.breachNumerationToShow,
                    breachTitle = r.breachTitle,
                    title = r.title,
                    description = r.description,
                    startDate = r.startDate,
                    dueDate = r.dueDate,
                    actionPlanStatusId = r.actionPlanStatusId,
                    statusName = r.statusName,
                    statusColor = r.statusColor,
                    actionPlanPriorityId = r.actionPlanPriorityId,
                    priorityName = r.priorityName,
                    priorityColor = r.priorityColor,
                    requirementEvaluationId = r.breachType == "1"
                        ? reqEvaluations.FirstOrDefault(re => re.requirementId == r.breachRequirementId)?.requirementEvaluationId
                        : null,
                    controlEvaluationId = r.breachType == "2"
                        ? ctrlEvaluations.FirstOrDefault(ce => ce.controlId == r.breachControlId)?.controlEvaluationId
                        : null,
                }).ToList();

                BaseResponseDto<GetActionPlansByUserIdDto> baseResponseDto = new BaseResponseDto<GetActionPlansByUserIdDto>();
                baseResponseDto.data = data;
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
