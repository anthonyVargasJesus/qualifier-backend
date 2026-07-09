namespace Qualifier.Application.Database.Gap.Queries.GetEvaluacionBootstrap
{
    public interface IGetEvaluacionBootstrapQuery
    {
        Task<Object> Execute(int companyId, int userId);
    }
}
