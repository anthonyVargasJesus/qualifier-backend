using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class SectionEntity : BaseEntity
    {
        public int sectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public int documentationId { get; set; }
        public int? versionId { get; set; }
        public int companyId { get; set; }
        public DocumentationEntity documentation { get; set; }
        public VersionEntity version { get; set; }
        [NotMapped]
        public List<SectionEntity> children { get; set; }
    }
}

