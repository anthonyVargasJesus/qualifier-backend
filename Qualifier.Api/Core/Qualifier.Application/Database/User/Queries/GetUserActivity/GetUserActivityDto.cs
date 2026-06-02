namespace Qualifier.Application.Database.User.Queries.GetUserActivity
{
    public class GetUserActivityDto
    {
        public int totalUsers { get; set; }
        public int totalActive { get; set; }
        public int totalInactive { get; set; }
        public int neverAccessed { get; set; }
        public int notAccessedIn30Days { get; set; }
        public List<GetUserActivityUserDto> users { get; set; } = new();
        public List<GetUserActivityPieDto> userStateChart { get; set; } = new();
        public List<GetUserActivityPieDto> accessFrequencyChart { get; set; } = new();
        public List<GetUserActivityBarDto> roleDistributionChart { get; set; } = new();
    }

    public class GetUserActivityUserDto
    {
        public int userId { get; set; }
        public string? name { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? image { get; set; }
        public string rolesText { get; set; } = string.Empty;
        public string? userStateName { get; set; }
        public int userStateValue { get; set; }
        public DateTime? lastAccess { get; set; }
        public int? daysSinceLastAccess { get; set; }
        public string accessBucket { get; set; } = string.Empty;
    }

    public class GetUserActivityPieDto
    {
        public string name { get; set; } = string.Empty;
        public int value { get; set; }
    }

    public class GetUserActivityBarDto
    {
        public string name { get; set; } = string.Empty;
        public int value { get; set; }
    }
}
