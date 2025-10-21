using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RiskAssessmentConfiguration
    {
        public RiskAssessmentConfiguration(EntityTypeBuilder<RiskAssessmentEntity> entityBuilder)
        {
            entityBuilder.ToTable("RiskAssessment");
            entityBuilder.HasKey(x => x.riskAssessmentId);
            entityBuilder.Property(x => x.riskAssessmentId).IsRequired();
            entityBuilder.Property(x => x.riskId).IsRequired();
            entityBuilder.Property(x => x.valuationCID).IsRequired();
            entityBuilder.Property(x => x.menaceLevelValue).IsRequired();
            entityBuilder.Property(x => x.vulnerabilityLevelValue).IsRequired();
            entityBuilder.Property(x => x.existingImplementedControls).IsRequired();
            entityBuilder.Property(x => x.riskAssessmentValue).IsRequired();
            entityBuilder.Property(x => x.riskLevelId).IsRequired();

        }
    }
}


