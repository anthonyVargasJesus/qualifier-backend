namespace Qualifier.Application.Database.Policy.Queries.GetPolicyById
{
    public interface IGetPolicyByIdQuery
    {
        Task<Object> Execute(int policyId);
    }
}

