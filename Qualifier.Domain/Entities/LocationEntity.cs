using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class LocationEntity : BaseEntity
    {
        public int locationId { get; set; }
        public string abbreviation { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
    }
}

