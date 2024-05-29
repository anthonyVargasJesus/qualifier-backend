using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class OwnerEntity : BaseEntity
    {
        public int ownerId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
    }
}

