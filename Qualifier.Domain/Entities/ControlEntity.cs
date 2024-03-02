using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ControlEntity : BaseEntity
    {
        public int controlId { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int controlGroupId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public ControlGroupEntity controlGroup { get; set; }
        public StandardEntity standard { get; set; }
    }
}

