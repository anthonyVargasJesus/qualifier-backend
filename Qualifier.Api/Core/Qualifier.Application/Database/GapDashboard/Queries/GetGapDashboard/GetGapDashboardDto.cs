namespace Qualifier.Application.Database.GapDashboard.Queries.GetGapDashboard
{
    public class GetGapDashboardDto
    {
        public int totalItems { get; set; }
        public int evaluatedItems { get; set; }
        public int compliantItems { get; set; }
        public int compliancePercentage { get; set; }
        public int openBreachesCount { get; set; }
        public int overdueActionPlansCount { get; set; }
        public int evaluatedItemsWithEvidenceCount { get; set; }
        public List<GetGapDashboardMaturityCountDto> maturityCounts { get; set; } = new();
        public List<GetGapDashboardThemeDto> themes { get; set; } = new();
        public List<GetGapDashboardPendingItemDto> pendingItems { get; set; } = new();
        public List<GetGapDashboardBreachSeverityDto> breachSeverityBreakdown { get; set; } = new();
    }

    public class GetGapDashboardBreachSeverityDto
    {
        public string name { get; set; } = string.Empty;
        public string color { get; set; } = string.Empty;
        public int count { get; set; }
    }

    public class GetGapDashboardMaturityCountDto
    {
        public string name { get; set; } = string.Empty;
        public int count { get; set; }
    }

    public class GetGapDashboardThemeDto
    {
        public string theme { get; set; } = string.Empty;
        public int totalItems { get; set; }
        public int evaluatedItems { get; set; }
        public int compliantItems { get; set; }
        public int percentage { get; set; }
    }

    public class GetGapDashboardPendingItemDto
    {
        public string tipo { get; set; } = string.Empty; // "control" | "requisito"
        public int itemId { get; set; }
        public string code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string theme { get; set; } = string.Empty;
    }
}
