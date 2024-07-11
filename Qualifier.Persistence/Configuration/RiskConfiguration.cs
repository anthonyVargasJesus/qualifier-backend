using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class RiskConfiguration
    {
        public RiskConfiguration(EntityTypeBuilder<RiskEntity> entityBuilder)
        {
            entityBuilder.ToTable("Risk");
            entityBuilder.HasKey(x => x.riskId);
            entityBuilder.Property(x => x.riskId).IsRequired();
            entityBuilder.Property(x => x.activesInventoryId).IsRequired();
            entityBuilder.Property(x => x.menaceId).IsRequired();
            entityBuilder.Property(x => x.vulnerabilityId).IsRequired();
            entityBuilder.Property(x => x.menaceLevel).IsRequired();
            entityBuilder.Property(x => x.vulnerabilityLevel).IsRequired();
            entityBuilder.Property(x => x.controlId).IsRequired();
            entityBuilder.Property(x => x.riskAssessmentValue).IsRequired();
            entityBuilder.Property(x => x.riskLevelId).IsRequired();
            entityBuilder.Property(x => x.treatmentMethod).IsRequired();
            entityBuilder.Property(x => x.controlTypeId).IsRequired();
            entityBuilder.Property(x => x.riskAssessmentValueWithTreatment).IsRequired();
            entityBuilder.Property(x => x.riskLevelWithImplementedControlld).IsRequired();
            entityBuilder.Property(x => x.residualRisk).IsRequired();

        }
    }
}


