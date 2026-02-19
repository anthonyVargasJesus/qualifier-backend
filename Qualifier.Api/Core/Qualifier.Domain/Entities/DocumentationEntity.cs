using System.ComponentModel.DataAnnotations.Schema;
using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class DocumentationEntity : BaseEntity
    {
        public int documentationId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string? template { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public int documentTypeId { get; set; }
        public StandardEntity standard { get; set; }
        public DocumentTypeEntity documentType { get; set; }

        [NotMapped]
        public List<RequirementEntity> requirements { get; set; }
        [NotMapped]
        public List<ControlEntity> controls { get; set; }
    }
}

