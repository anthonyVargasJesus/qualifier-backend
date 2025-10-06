namespace Qualifier.Application.Database.ActionPlan.Commands.DeleteActionPlan
{
    public interface IDeleteActionPlanCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

