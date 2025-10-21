namespace Qualifier.Application.Database.ActionPlanStatus.Commands.UpdateActionPlanStatus
{
    public interface IUpdateActionPlanStatusCommand
    {
        Task<Object> Execute(UpdateActionPlanStatusDto model, int id);
    }
}

