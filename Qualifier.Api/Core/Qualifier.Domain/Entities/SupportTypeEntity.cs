using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class SupportTypeEntity : BaseEntity
    {
        public int supportTypeId { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
    }
}

