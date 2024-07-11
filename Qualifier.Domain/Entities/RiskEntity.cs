using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class RiskEntity : BaseEntity
    {
        public int riskId { get; set; }
        public int activesInventoryId { get; set; }
        public string? activesInventoryNumber { get; set; }
        public string? macroProcess { get; set; }
        public string? subProcess { get; set; }
        public string? activesInventoryName { get; set; }
        public decimal? activesInventoryValuation { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public decimal menaceLevel { get; set; }
        public decimal vulnerabilityLevel { get; set; }
        public int controlId { get; set; }
        public decimal riskAssessmentValue { get; set; }
        public int riskLevelId { get; set; }
        public string treatmentMethod { get; set; }
        public int controlTypeId { get; set; }
        public string? controlsToImplement { get; set; }
        public decimal? menaceLevelWithTreatment { get; set; }
        public decimal? vulnerabilityLevelWithTreatment { get; set; }
        public decimal riskAssessmentValueWithTreatment { get; set; }
        public int riskLevelWithImplementedControlld { get; set; }
        public string residualRisk { get; set; }
        public string? activities { get; set; }
        public DateTime? implementationStartDate { get; set; }
        public DateTime? verificationDate { get; set; }
        public int? responsibleId { get; set; }
        public string? observation { get; set; }
        public int? companyId { get; set; }
        public ActivesInventoryEntity activesInventory { get; set; }
        public MenaceEntity menace { get; set; }
        public VulnerabilityEntity vulnerability { get; set; }
        public ControlEntity control { get; set; }
        public RiskLevelEntity riskLevel { get; set; }
        public ControlTypeEntity controlType { get; set; }
        public ResponsibleEntity? responsible { get; set; }
    }
}

