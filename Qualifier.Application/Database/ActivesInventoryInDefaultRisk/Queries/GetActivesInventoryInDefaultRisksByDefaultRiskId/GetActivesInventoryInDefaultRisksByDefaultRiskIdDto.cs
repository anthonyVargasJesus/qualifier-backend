namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetActivesInventoryInDefaultRisksByDefaultRiskId
{
public class GetActivesInventoryInDefaultRisksByDefaultRiskIdDto
{
public int activesInventoryInDefaultRiskId { get; set; }
public int defaultRiskId { get; set; }
public int activesInventoryId { get; set; }
public bool isActive { get; set; }
 public GetActivesInventoryInDefaultRisksByDefaultRiskIdActivesInventoryDto? activesInventory { get; set; }
}
public class GetActivesInventoryInDefaultRisksByDefaultRiskIdActivesInventoryDto
{
public string name { get; set; }

}
}

