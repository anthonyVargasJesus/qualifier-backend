using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class DocumentationEntity : BaseEntity
    {
        public int documentationId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string template { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public ICollection<ReferenceDocumentationEntity> referenceDocumentations { get; set; }
    }
}

