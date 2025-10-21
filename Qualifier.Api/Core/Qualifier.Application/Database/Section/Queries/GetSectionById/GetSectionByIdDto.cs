namespace Qualifier.Application.Database.Section.Queries.GetSectionById
{
    public class GetSectionByIdDto
    {
        public int sectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public int documentationId { get; set; }
        public int versionId { get; set; }
        public int companyId { get; set; }

    }
}

