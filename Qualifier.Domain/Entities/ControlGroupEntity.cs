using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ControlGroupEntity : BaseEntity
    {
        public int controlGroupId { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? standardId { get; set; }
        public int? companyId { get; set; }
    }
}

