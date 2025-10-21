namespace Qualifier.Application.Database.Option.Queries.GetOptionsByCompanyId
{
    public class GetOptionsByCompanyIdDto
    {
        public int optionId { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public bool isMobile { get; set; }

    }
}

