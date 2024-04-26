namespace Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionById
{
    public class GetDefaultSectionByIdDto
    {
        public int defaultSectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public int documentTypeId { get; set; }

    }
}

