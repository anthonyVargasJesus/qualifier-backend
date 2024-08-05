using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public int companyId { get; set; }
        public string name { get; set; }
        public string? abbreviation { get; set; }
        public string? slogan { get; set; }
        public string? logo { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
    }
}

