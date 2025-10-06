namespace Qualifier.Application.Database.ActionPlanStatus.Commands.CreateActionPlanStatus
{
    public interface ICreateActionPlanStatusCommand
    {
        Task<Object> Execute(CreateActionPlanStatusDto model);
    }
}

