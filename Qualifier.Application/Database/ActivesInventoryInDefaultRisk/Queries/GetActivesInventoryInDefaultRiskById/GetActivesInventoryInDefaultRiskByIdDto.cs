using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetActivesInventoryInDefaultRiskById
{
public class GetActivesInventoryInDefaultRiskByIdDto
{
public int activesInventoryInDefaultRiskId { get; set; }
public int defaultRiskId { get; set; }
public int activesInventoryId { get; set; }
public bool? isActive { get; set; }

}
}

