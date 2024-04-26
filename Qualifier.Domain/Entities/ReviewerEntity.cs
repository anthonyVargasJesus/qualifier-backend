using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ReviewerEntity : BaseEntity
    {
        public int reviewerId { get; set; }
        public int personalId { get; set; }
        public int responsibleId { get; set; }
        public int versionId { get; set; }
        public int documentationId { get; set; }
        public int companyId { get; set; }
        public PersonalEntity personal { get; set; }
        public ResponsibleEntity responsible { get; set; }
        public VersionEntity version { get; set; }
    }
}

