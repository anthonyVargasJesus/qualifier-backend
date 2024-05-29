using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ActiveTypeEntity : BaseEntity
    {
        public int activeTypeId { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
    }
}

