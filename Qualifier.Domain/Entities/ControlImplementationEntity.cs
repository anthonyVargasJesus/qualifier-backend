using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ControlImplementationEntity : BaseEntity
    {
        public int controlImplementationId { get; set; }
        public int? riskId { get; set; }
        public string activities { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? verificationDate { get; set; }
        public int responsibleId { get; set; }
        public string? observation { get; set; }
        public int? companyId { get; set; }
        public RiskEntity? risk { get; set; }
        public ResponsibleEntity responsible { get; set; }
    }
}

