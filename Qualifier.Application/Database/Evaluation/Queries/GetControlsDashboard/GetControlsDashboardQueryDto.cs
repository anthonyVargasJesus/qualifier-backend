using Qualifier.Application.Database.Evaluation.Queries.GetDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Evaluation.Queries.GetControlsDashboard
{
    public class GetControlDashboardDto
    {
        public List<GetControlDashboardControlGroupDto> controlGroups { get; set; }
        public List<GetControlDashboardMaturityLevelDto> maturityLevels { get; set; }
        public decimal? value { get; set; }
        public List<GetPieControlDashboardControlGroupDto> pieControlDashboardControlGroup { get; set; }
        public List<GetBartVerticalControlDashboardDto> bartVerticalDashboardControlGroupDto { get; set; }
        public List<string>? colors { get; set; }
    }

    public class GetControlDashboardControlGroupDto
    {
        public int controlGroupId { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public decimal? value { get; set; }
        public GetControlDashboardMaturityLevelInControlIndicatorDto indicator { get; set; }
        public List<GetDashboardMaturityLevelInControlDto> maturityLevels { get; set; }
    }
    public class GetControlDashboardMaturityLevelInControlIndicatorDto
    {
        public int indicatorId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public string color { get; set; }

    }
    public class GetDashboardMaturityLevelInControlDto
    {
        public int maturityLevelId { get; set; }
        public decimal? value { get; set; }
    }


    public class GetControlDashboardMaturityLevelDto
    {
        public int maturityLevelId { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public decimal? value { get; set; }
        public string color { get; set; }
        public decimal? percent { get; set; }
    }

    public class GetPieControlDashboardControlGroupDto
    {
        public string name { get; set; }
        public decimal value { get; set; }
    }

    public class GetBartVerticalControlDashboardDto
    {
        public string name { get; set; }
        public List<GetDashboardControlGroupSerieDto> series { get; set; }
    }

    public class GetDashboardControlGroupSerieDto
    {
        public string name { get; set; }
        public decimal value { get; set; }
    }

}
