using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class RiskEntity : BaseEntity
    {
        public int riskId { get; set; }
        public int evaluationId { get; set; }
        public int activesInventoryId { get; set; }
        public string? activesInventoryNumber { get; set; }
        public string? activesInventoryName { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public int? companyId { get; set; }
        public EvaluationEntity evaluation { get; set; }
        public ActivesInventoryEntity activesInventory { get; set; }
        public MenaceEntity menace { get; set; }
        public VulnerabilityEntity vulnerability { get; set; }
    }
}

