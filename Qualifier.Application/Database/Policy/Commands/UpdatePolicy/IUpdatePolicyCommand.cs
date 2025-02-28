namespace Qualifier.Application.Database.Policy.Commands.UpdatePolicy
{
    public interface IUpdatePolicyCommand
    {
        Task<Object> Execute(UpdatePolicyDto model, int id);
    }
}

