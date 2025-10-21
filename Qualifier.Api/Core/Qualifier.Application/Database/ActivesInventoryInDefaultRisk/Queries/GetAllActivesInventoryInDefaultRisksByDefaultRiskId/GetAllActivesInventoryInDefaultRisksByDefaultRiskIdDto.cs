namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetAllActivesInventoryInDefaultRisksByDefaultRiskId
{
public class GetAllActivesInventoryInDefaultRisksByDefaultRiskIdDto
{
public int activesInventoryInDefaultRiskId { get; set; }
public int defaultRiskId { get; set; }
public int activesInventoryId { get; set; }
public bool isActive { get; set; }
 public GetAllActivesInventoryInDefaultRisksByDefaultRiskIdActivesInventoryDto? activesInventory { get; set; }
}
public class GetAllActivesInventoryInDefaultRisksByDefaultRiskIdActivesInventoryDto
{
public string name { get; set; }

}
}

