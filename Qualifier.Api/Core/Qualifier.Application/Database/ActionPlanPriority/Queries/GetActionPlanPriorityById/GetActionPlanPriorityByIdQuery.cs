using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlanPriority.Queries.GetActionPlanPriorityById
{
    public class GetActionPlanPriorityByIdQuery : IGetActionPlanPriorityByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActionPlanPriorityByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int actionPlanPriorityId)
        {
            try
            {
                var entity = await (from item in _databaseService.ActionPlanPriority
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.actionPlanPriorityId == actionPlanPriorityId)
                                    select new ActionPlanPriorityEntity()
                                    {
                                        actionPlanPriorityId = item.actionPlanPriorityId,
                                        name = item.name,
                                        description = item.description,
                                        abbreviation = item.abbreviation,
                                        value = item.value,
                                        color = item.color,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetActionPlanPriorityByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

