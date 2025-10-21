using Qualifier.Application.Database.Section.Queries.GetSectionsByVersionId;

namespace Qualifier.Application.Database.Section.Queries.GetSectionsByDocumentationId
{
    public class GetSectionsByDocumentationIdDto
    {
        public int sectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public string numerationToShow { get; set; }
        public List<GetSectionsByDocumentationIdDto> children { get; set; }
    }
}
