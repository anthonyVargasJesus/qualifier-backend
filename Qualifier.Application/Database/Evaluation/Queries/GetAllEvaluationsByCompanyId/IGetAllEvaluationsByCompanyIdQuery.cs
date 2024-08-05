namespace Qualifier.Application.Database.Evaluation.Queries.GetAllEvaluationsByCompanyId
{
    public interface IGetAllEvaluationsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

