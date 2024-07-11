using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class MenaceEntity : BaseEntity
    {
        public int menaceId { get; set; }
        public int menaceTypeId { get; set; }
        public string name { get; set; }
        public int? companyId { get; set; }
        public MenaceTypeEntity menaceType { get; set; }
    }
}

