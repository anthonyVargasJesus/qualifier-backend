
namespace Qualifier.Domain.Entities
{
    public class BartVerticalDashboardRequirement
    {
        public string name { get; set; } = string.Empty;
        public List<DashboardRequirementSerie>? series { get; set; }
    }

    public class DashboardRequirementSerie
    {
        public string name { get; set; } = string.Empty;
        public decimal value { get; set; }
    }
}
