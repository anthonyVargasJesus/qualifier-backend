
namespace Qualifier.Application.Database.Evaluation.Queries.GetExcelDashboard
{
    public interface IGetExcelDashboardQuery
    {
        Task<Object> Execute(int standardId, int evaluationId, int companyId);
    }
}
