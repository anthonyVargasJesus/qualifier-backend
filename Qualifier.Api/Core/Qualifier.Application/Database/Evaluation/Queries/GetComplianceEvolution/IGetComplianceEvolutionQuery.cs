namespace Qualifier.Application.Database.Evaluation.Queries.GetComplianceEvolution
{
    public interface IGetComplianceEvolutionQuery
    {
        Task<Object> Execute(int companyId);
    }
}
