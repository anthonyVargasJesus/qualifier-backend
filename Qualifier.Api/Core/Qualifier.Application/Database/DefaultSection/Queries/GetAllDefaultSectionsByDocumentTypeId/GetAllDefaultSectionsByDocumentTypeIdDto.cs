namespace Qualifier.Application.Database.DefaultSection.Queries.GetAllDefaultSectionsByDocumentTypeId
{
    public class GetAllDefaultSectionsByDocumentTypeIdDto
    {
        public int defaultSectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public string numerationToShow { get; set; }
    }
}

