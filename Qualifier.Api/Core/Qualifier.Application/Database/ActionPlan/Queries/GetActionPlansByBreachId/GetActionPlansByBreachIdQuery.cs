using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByBreachId
{
    public class GetActionPlansByBreachIdQuery : IGetActionPlansByBreachIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActionPlansByBreachIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int breachId)
        {
            try
            {
                var entities = await (from actionPlan in _databaseService.ActionPlan
                                      join actionPlanPriority in _databaseService.ActionPlanPriority on actionPlan.actionPlanPriorityId equals actionPlanPriority.actionPlanPriorityId
                                      join actionPlanStatus in _databaseService.ActionPlanStatus on actionPlan.actionPlanStatusId equals actionPlanStatus.actionPlanStatusId
                                      join user in _databaseService.User on actionPlan.userId equals user.userId into userJoin
                                      from user in userJoin.DefaultIfEmpty()
                                      where ((actionPlan.isDeleted == null || actionPlan.isDeleted == false) && actionPlan.breachId == breachId)
                                      && (actionPlan.title.ToUpper().Contains(search.ToUpper()))
                                      select new ActionPlanEntity
                                      {
                                          actionPlanId = actionPlan.actionPlanId,
                                          breachId = actionPlan.breachId,
                                          evaluationId = actionPlan.evaluationId,
                                          standardId = actionPlan.standardId,
                                          title = actionPlan.title,
                                          description = actionPlan.description,
                                          userId = actionPlan.userId,
                                          startDate = actionPlan.startDate,
                                          dueDate = actionPlan.dueDate,
                                          actionPlanStatusId = actionPlan.actionPlanStatusId,
                                          actionPlanPriorityId = actionPlan.actionPlanPriorityId,
                                          actionPlanPriority = new ActionPlanPriorityEntity
                                          {
                                              name = actionPlanPriority.name,
                                              description = actionPlanPriority.description,
                                              color = actionPlanPriority.color,
                                          },
                                          actionPlanStatus = new ActionPlanStatusEntity
                                          {
                                              name = actionPlanStatus.name,
                                              abbreviation = actionPlanStatus.abbreviation,
                                              color = actionPlanStatus.color,
                                          },
                                          user = user == null ? null : new UserEntity
                                          {
                                              name = (user.name ?? "") + " " + (user.firstName ?? ""),
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();


                BaseResponseDto<GetActionPlansByBreachIdDto> baseResponseDto = new BaseResponseDto<GetActionPlansByBreachIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetActionPlansByBreachIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, breachId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int breachId)
        {
            var total = await (from actionPlan in _databaseService.ActionPlan
                               join actionPlanPriority in _databaseService.ActionPlanPriority on actionPlan.actionPlanPriorityId equals actionPlanPriority.actionPlanPriorityId
                               join actionPlanStatus in _databaseService.ActionPlanStatus on actionPlan.actionPlanStatusId equals actionPlanStatus.actionPlanStatusId
                               where ((actionPlan.isDeleted == null || actionPlan.isDeleted == false) && actionPlan.breachId == breachId)
                               && (actionPlan.title.ToUpper().Contains(search.ToUpper()))
                               select new ActionPlanEntity
                               {
                                   actionPlanId = actionPlan.actionPlanId,
                               }).CountAsync();
            return total;
        }

    }
}

