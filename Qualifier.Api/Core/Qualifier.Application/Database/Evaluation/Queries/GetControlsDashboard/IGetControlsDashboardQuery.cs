
namespace Qualifier.Application.Database.Evaluation.Queries.GetControlsDashboard
{
    public interface IGetControlsDashboardQuery
    {
        Task<Object> Execute(int standardId, int evaluationId, int companyId);
    }
}
