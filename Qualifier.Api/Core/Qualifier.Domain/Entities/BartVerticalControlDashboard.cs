
namespace Qualifier.Domain.Entities
{
    public class BartVerticalControlDashboard
    {
        public string name { get; set; } = string.Empty;
        public List<DashboardControlGroupSerie>? series { get; set; }
    }

    public class DashboardControlGroupSerie
    {
        public string name { get; set; } = string.Empty;
        public decimal value { get; set; }
    }

}
