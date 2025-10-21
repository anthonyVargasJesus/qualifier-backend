using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RiskTreatmentConfiguration
    {
        public RiskTreatmentConfiguration(EntityTypeBuilder<RiskTreatmentEntity> entityBuilder)
        {
            entityBuilder.ToTable("RiskTreatment");
            entityBuilder.HasKey(x => x.riskTreatmentId);
            entityBuilder.Property(x => x.riskTreatmentId).IsRequired();
            entityBuilder.Property(x => x.riskId).IsRequired();
            entityBuilder.Property(x => x.riskTreatmentMethodId).IsRequired();
            entityBuilder.Property(x => x.controlType).IsRequired();

        }
    }
}


