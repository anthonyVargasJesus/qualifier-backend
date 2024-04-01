namespace Qualifier.Application.Database.Evaluation.Queries.GetEvaluationsByCompanyId
{
    public interface IGetEvaluationsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

