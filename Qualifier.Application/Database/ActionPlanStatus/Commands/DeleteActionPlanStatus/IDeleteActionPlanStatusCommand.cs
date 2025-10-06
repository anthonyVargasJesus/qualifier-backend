namespace Qualifier.Application.Database.ActionPlanStatus.Commands.DeleteActionPlanStatus
{
    public interface IDeleteActionPlanStatusCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

