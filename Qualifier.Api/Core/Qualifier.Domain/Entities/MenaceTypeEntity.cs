using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class MenaceTypeEntity : BaseEntity
    {
        public int menaceTypeId { get; set; }
        public string name { get; set; }
        public int? companyId { get; set; }
    }
}

