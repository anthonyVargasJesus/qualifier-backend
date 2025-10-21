namespace Qualifier.Application.Database.Section.Queries.GetAllSectionsByVersionId
{
    public class GetAllSectionsByVersionIdDto
    {
        public int sectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public string numerationToShow { get; set; }

    }
}

