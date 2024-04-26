using Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId;

namespace Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionsByDocumentTypeId
{
    public class GetDefaultSectionsByDocumentTypeIdDto
    {
        public int defaultSectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public List<GetDefaultSectionsByDocumentTypeIdDto> children { get; set; }
    }

}
