namespace Qualifier.Application.Database.ActionPlanPriority.Commands.CreateActionPlanPriority
{
    public interface ICreateActionPlanPriorityCommand
    {
        Task<Object> Execute(CreateActionPlanPriorityDto model);
    }
}

