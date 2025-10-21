using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class UsageClassificationEntity : BaseEntity
    {
        public int usageClassificationId { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
    }
}

