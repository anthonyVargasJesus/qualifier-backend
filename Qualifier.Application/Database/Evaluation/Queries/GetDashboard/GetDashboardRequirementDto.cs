using Qualifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Evaluation.Queries.GetDashboard
{

    public class GetDashboardDto
    {
        public List<GetDashboardRequirementDto> requirements { get; set; }
        public List<GetDashboardMaturityLevelDto> maturityLevels { get; set; }
        public decimal? value { get; set; }
        public List<GetPieDashboardRequirementDto> pieDashboardRequirementDto { get; set; }
        public List<GetBartVerticalDashboardRequirementDto> bartVerticalDashboardRequirementDto { get; set; }
        public List<string>? colors { get; set; }
    }

    public class GetPieDashboardRequirementDto
    {
        public string name { get; set; }
        public decimal value { get; set; }
    }
    public class GetBartVerticalDashboardRequirementDto
    {
        public string name { get; set; }
        public List<GetDashboardRequirementSerieDto> series { get; set; }
    }

    public class GetDashboardRequirementSerieDto
    {
        public string name { get; set; }
        public decimal value { get; set; }
    }

    public class GetDashboardRequirementDto
    {
        public int requirementId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public decimal? value { get; set; }
        public GetDashboardMaturityLevelInRequirementIndicatorDto indicator { get; set; }
        public List<GetDashboardMaturityLevelInRequirementDto> maturityLevels { get; set; }    
    }

    public class GetDashboardMaturityLevelDto
    {
        public int maturityLevelId { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public decimal? value { get; set; }
        public string color { get; set; }
        public decimal? percent { get; set; }
    }

    public class GetDashboardMaturityLevelInRequirementDto
    {
        public int maturityLevelId { get; set; }
        public decimal? value { get; set; }
    }

    public class GetDashboardMaturityLevelInRequirementIndicatorDto
    {
        public int indicatorId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public string color { get; set; }

    }

}
