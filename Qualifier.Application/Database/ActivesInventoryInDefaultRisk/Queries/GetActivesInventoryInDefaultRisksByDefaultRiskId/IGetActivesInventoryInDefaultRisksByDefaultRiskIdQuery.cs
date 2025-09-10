namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetActivesInventoryInDefaultRisksByDefaultRiskId
{
public interface IGetActivesInventoryInDefaultRisksByDefaultRiskIdQuery
{
Task<Object> Execute(int skip, int pageSize, string search, int defaultRiskId);
}
}

