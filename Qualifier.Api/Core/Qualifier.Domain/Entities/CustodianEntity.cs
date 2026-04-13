using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class CustodianEntity : BaseEntity
    {
        public int custodianId { get; set; }
        public string code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public int companyId { get; set; }
    }
}

