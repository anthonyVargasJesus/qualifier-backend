namespace Qualifier.Application.Database.ActionPlanPriority.Commands.UpdateActionPlanPriority
{
    public interface IUpdateActionPlanPriorityCommand
    {
        Task<Object> Execute(UpdateActionPlanPriorityDto model, int id);
    }
}

