
namespace Qualifier.Domain.Entities
{
    public class BartVerticalControlDashboard
    {
        public string name { get; set; }
        public List<DashboardControlGroupSerie> series { get; set; }
    }

    public class DashboardControlGroupSerie
    {
        public string name { get; set; }
        public decimal value { get; set; }
    }

}
