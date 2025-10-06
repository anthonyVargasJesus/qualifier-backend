namespace Qualifier.Application.Database.ActionPlan.Commands.UpdateActionPlan
{
    public interface IUpdateActionPlanCommand
    {
        Task<Object> Execute(UpdateActionPlanDto model, int id);
    }
}

