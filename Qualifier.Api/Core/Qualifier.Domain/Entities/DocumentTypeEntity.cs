using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class DocumentTypeEntity : BaseEntity
    {
        public int documentTypeId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int companyId { get; set; }

        public ICollection<DocumentationEntity> documentations { get; set; }
    }
}

