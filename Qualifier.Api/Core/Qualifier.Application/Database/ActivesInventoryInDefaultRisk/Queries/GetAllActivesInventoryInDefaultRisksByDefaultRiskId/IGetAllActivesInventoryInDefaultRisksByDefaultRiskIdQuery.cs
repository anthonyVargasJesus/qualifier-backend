namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetAllActivesInventoryInDefaultRisksByDefaultRiskId
{
public interface IGetAllActivesInventoryInDefaultRisksByDefaultRiskIdQuery
{
Task<Object> Execute(int defaultRiskId);
}
}

