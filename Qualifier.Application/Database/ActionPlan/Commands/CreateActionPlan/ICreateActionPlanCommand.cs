namespace Qualifier.Application.Database.ActionPlan.Commands.CreateActionPlan
{
    public interface ICreateActionPlanCommand
    {
        Task<Object> Execute(CreateActionPlanDto model);
    }
}

