using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanById
{
    public class GetActionPlanByIdQuery : IGetActionPlanByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActionPlanByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int actionPlanId)
        {
            try
            {
                var entity = await (from item in _databaseService.ActionPlan
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.actionPlanId == actionPlanId)
                                    select new ActionPlanEntity()
                                    {
                                        actionPlanId = item.actionPlanId,
                                        breachId = item.breachId,
                                        evaluationId = item.evaluationId,
                                        standardId = item.standardId,
                                        title = item.title,
                                        description = item.description,
                                        responsibleId = item.responsibleId,
                                        startDate = item.startDate,
                                        dueDate = item.dueDate,
                                        actionPlanStatusId = item.actionPlanStatusId,
                                        actionPlanPriorityId = item.actionPlanPriorityId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetActionPlanByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

