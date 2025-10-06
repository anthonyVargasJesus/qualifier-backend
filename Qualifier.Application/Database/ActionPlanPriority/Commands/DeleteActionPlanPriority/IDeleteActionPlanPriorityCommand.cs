namespace Qualifier.Application.Database.ActionPlanPriority.Commands.DeleteActionPlanPriority
{
    public interface IDeleteActionPlanPriorityCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

