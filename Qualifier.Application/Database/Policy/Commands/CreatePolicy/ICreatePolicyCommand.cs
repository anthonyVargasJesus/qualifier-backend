namespace Qualifier.Application.Database.Policy.Commands.CreatePolicy
{
    public interface ICreatePolicyCommand
    {
        Task<Object> Execute(CreatePolicyDto model);
    }
}

