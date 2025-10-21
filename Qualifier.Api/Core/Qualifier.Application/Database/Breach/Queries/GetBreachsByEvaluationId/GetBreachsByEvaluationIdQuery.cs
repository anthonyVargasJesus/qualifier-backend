using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Breach.Queries.GetBreachsByEvaluationId
{
    public class GetBreachsByEvaluationIdQuery : IGetBreachsByEvaluationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetBreachsByEvaluationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int evaluationId, int companyId)
        {
            try
            {
                var breachs = await (from breach in _databaseService.Breach
                                      join breachSeverity in _databaseService.BreachSeverity on breach.breachSeverity equals breachSeverity
                                      join breachStatus in _databaseService.BreachStatus on breach.breachStatus equals breachStatus
                                      join bt2 in _databaseService.Control on breach.controlId equals bt2.controlId into _bt2
                                      from control in _bt2.DefaultIfEmpty()
                                      join bt in _databaseService.Requirement on breach.requirementId equals bt.requirementId into _bt
                                      from requirement in _bt.DefaultIfEmpty()
                                      join responsible in _databaseService.Responsible on breach.responsible equals responsible
                                      where ((breach.isDeleted == null || breach.isDeleted == false) && breach.evaluationId == evaluationId)
                                      && (breach.title.ToUpper().Contains(search.ToUpper()))
                                      select new BreachEntity
                                      {
                                          breachId = breach.breachId,
                                          type = breach.type,
                                          requirementId = breach.requirementId,
                                          controlId = breach.controlId,
                                          numerationToShow = breach.numerationToShow,
                                          title = breach.title,
                                          breachSeverityId = breach.breachSeverityId,
                                          breachStatusId = breach.breachStatusId,
                                          responsibleId = breach.responsibleId,
                                          breachSeverity = new BreachSeverityEntity
                                          {
                                              name = breachSeverity.name,
                                              color = breachSeverity.color,
                                          },
                                          breachStatus = new BreachStatusEntity
                                          {
                                              name = breachStatus.name,
                                              color = breachStatus.color,
                                          },
                                          control = (control != null) ? new ControlEntity
                                          {
                                              number = control.number,
                                              name = control.name,
                                          } : null,
                                          requirement = (requirement != null) ? new RequirementEntity
                                          {
                                              numeration = requirement.numeration,
                                              name = requirement.name,
                                          } : null,
                                          responsible = new ResponsibleEntity
                                          {
                                              name = responsible.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                var actionPlans = await (from actionPlan in _databaseService.ActionPlan
                                      where ((actionPlan.isDeleted == null || actionPlan.isDeleted == false) && actionPlan.evaluationId == evaluationId)
                                      && (actionPlan.title.ToUpper().Contains(search.ToUpper()))
                                      select new ActionPlanEntity
                                      {
                                          actionPlanId = actionPlan.actionPlanId,
                                          breachId = actionPlan.breachId,
                                          actionPlanStatusId = actionPlan.actionPlanStatusId,
                                      }).ToListAsync();

                var status = await (from actionPlanStatus in _databaseService.ActionPlanStatus
                                      where ((actionPlanStatus.isDeleted == null || actionPlanStatus.isDeleted == false) 
                                      && actionPlanStatus.companyId == companyId)
                                      select new ActionPlanStatusEntity
                                      {
                                          actionPlanStatusId = actionPlanStatus.actionPlanStatusId,
                                          name = actionPlanStatus.name,
                                          value = actionPlanStatus.value,
                                          color = actionPlanStatus.color,
                                      }).ToListAsync();

                foreach (var breach in breachs)
                {
                    var plans = actionPlans.Where(p => p.breachId == breach.breachId).ToList();

                    if (plans.Any())
                    {
                        var parts = new List<string>();

                        foreach (var s in status.OrderBy(s => s.value)) // 👈 orden por prioridad
                        {
                            int count = plans.Count(p => p.actionPlanStatusId == s.actionPlanStatusId);
                            if (count > 0)
                            {
                                string nameFormatted = s.name.ToLower();
                                if (count > 1 && !nameFormatted.EndsWith("s"))
                                    nameFormatted += "s";

                                parts.Add($"<span class='action-plan-status'><span class='dot' style='background-color:{s.color}'></span> {count} {nameFormatted}</span>");
                            }
                        }

                        breach.actionPlanSummary = string.Join(", ", parts);
                    }
                    else
                    {
                        breach.actionPlanSummary = "<span class='action-plan-status' style='color:#aaa;'>Sin planes de acción</span>";
                    }
                }


                BaseResponseDto<GetBreachsByEvaluationIdDto> baseResponseDto = new BaseResponseDto<GetBreachsByEvaluationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetBreachsByEvaluationIdDto>>(breachs);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, evaluationId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int evaluationId)
        {
            var total = await (from breach in _databaseService.Breach
                               join breachSeverity in _databaseService.BreachSeverity on breach.breachSeverity equals breachSeverity
                               join breachStatus in _databaseService.BreachStatus on breach.breachStatus equals breachStatus
                               join bt2 in _databaseService.Control on breach.controlId equals bt2.controlId into _bt2
                               from control in _bt2.DefaultIfEmpty()
                               join bt in _databaseService.Requirement on breach.requirementId equals bt.requirementId into _bt
                               from requirement in _bt.DefaultIfEmpty()
                               join responsible in _databaseService.Responsible on breach.responsible equals responsible
                               where ((breach.isDeleted == null || breach.isDeleted == false) && breach.evaluationId == evaluationId)
                               && (breach.title.ToUpper().Contains(search.ToUpper()))
                               select new BreachEntity
                               {
                                   breachId = breach.breachId,
                               }).CountAsync();
            return total;
        }

    }
}
