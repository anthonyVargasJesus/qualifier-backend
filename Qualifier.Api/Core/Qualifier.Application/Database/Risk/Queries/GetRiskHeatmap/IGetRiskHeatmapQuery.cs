namespace Qualifier.Application.Database.Risk.Queries.GetRiskHeatmap
{
    public interface IGetRiskHeatmapQuery
    {
        Task<Object> Execute(int companyId, int? evaluationId = null);
    }
}
