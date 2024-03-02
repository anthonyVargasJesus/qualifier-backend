using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class StandardEntity : BaseEntity
    {
        public int standardId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int parentId { get; set; }
        public int companyId { get; set; }
        public StandardEntity standard { get; set; }
        public ICollection<StandardEntity> standards { get; set; }
    }
}

