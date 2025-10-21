using System.ComponentModel.DataAnnotations.Schema;
using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class RiskEntity : BaseEntity
    {
        public int riskId { get; set; }
        public string name { get; set; } = string.Empty;
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
        public int? riskStatusId { get; set; }
        public RiskStatusEntity? riskStatus { get; set; }
        public int? breachId { get; set; }

        [NotMapped]
        public int riskAssessmentId { get; set; }
        [NotMapped]
        public int riskTreatmentId { get; set; }
        [NotMapped]
        public string? controlSummary { get;set; }
        [NotMapped]
        public int effectiveControls { get; set; }
        [NotMapped]// Controles efectivos (numerador)
        public int plannedControls { get; set; }            // Controles planificados (denominador)

        [NotMapped]
        public decimal initialRiskValue { get; set; }
        [NotMapped]// Nivel inicial numérico (ej. 28.02)
        public string? initialRiskLevel { get; set; }
        [NotMapped]// Nivel inicial numérico (ej. 28.02)
        public string? initialRiskColor { get; set; }
        [NotMapped]
        public decimal? treatedRiskValue { get; set; }
        [NotMapped]// Riesgo tratado numérico
        public string? treatedRiskLevel { get; set; }
        [NotMapped]
        public string? treatedRiskColor { get; set; }
        [NotMapped]
        public decimal? residualRiskValue { get; set; }     // Riesgo residual real numérico
        [NotMapped]
        public string? residualRiskLevel { get; set; }      // Riesgo residual real textual
        [NotMapped]
        public string? residualRiskColor { get; set; }
        [NotMapped]
        public string? trend { get; set; }
        [NotMapped]
        public string? trendIcon { get; set; }
    }
}

