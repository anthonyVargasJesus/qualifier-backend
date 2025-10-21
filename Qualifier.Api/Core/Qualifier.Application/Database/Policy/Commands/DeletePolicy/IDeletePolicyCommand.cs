namespace Qualifier.Application.Database.Policy.Commands.DeletePolicy
{
    public interface IDeletePolicyCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

