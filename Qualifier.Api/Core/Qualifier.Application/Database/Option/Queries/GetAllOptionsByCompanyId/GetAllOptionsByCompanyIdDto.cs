namespace Qualifier.Application.Database.Option.Queries.GetAllOptionsByCompanyId
{
    public class GetAllOptionsByCompanyIdDto
    {
        public int optionId { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public bool isMobile { get; set; }

    }
}

