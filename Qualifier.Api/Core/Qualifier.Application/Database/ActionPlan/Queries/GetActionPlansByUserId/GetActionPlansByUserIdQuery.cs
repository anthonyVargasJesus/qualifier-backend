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
                var data = await (from ap in _databaseService.ActionPlan
                                  join status in _databaseService.ActionPlanStatus on ap.actionPlanStatusId equals status.actionPlanStatusId
                                  join priority in _databaseService.ActionPlanPriority on ap.actionPlanPriorityId equals priority.actionPlanPriorityId
                                  join breach in _databaseService.Breach on ap.breachId equals breach.breachId
                                  where (ap.isDeleted == null || ap.isDeleted == false)
                                        && ap.userId == userId && ap.evaluationId == evaluationId
                                  orderby ap.dueDate
                                  select new GetActionPlansByUserIdDto
                                  {
                                      actionPlanId = ap.actionPlanId,
                                      breachId = ap.breachId,
                                      evaluationId = ap.evaluationId,
                                      standardId = ap.standardId,
                                      userId = ap.userId ?? 0,
                                      breachNumerationToShow = breach.numerationToShow ?? "",
                                      breachTitle = breach.title,
                                      title = ap.title,
                                      description = ap.description,
                                      startDate = ap.startDate,
                                      dueDate = ap.dueDate,
                                      actionPlanStatusId = ap.actionPlanStatusId,
                                      statusName = status.name,
                                      statusColor = status.color,
                                      actionPlanPriorityId = ap.actionPlanPriorityId,
                                      priorityName = priority.name,
                                      priorityColor = priority.color,
                                  }).ToListAsync();

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
