using Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionsByDocumentTypeId;

namespace Qualifier.Application.Database.Section.Queries.GetSectionsByVersionId
{
    public class GetSectionsByVersionIdDto
    {
        public int sectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public List<GetSectionsByVersionIdDto> children { get; set; }
    }

}
