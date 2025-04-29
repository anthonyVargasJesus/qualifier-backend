using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class RiskTreatmentMethodConfiguration
    {
        public RiskTreatmentMethodConfiguration(EntityTypeBuilder<RiskTreatmentMethodEntity> entityBuilder)
        {
            entityBuilder.ToTable("RiskTreatmentMethod");
            entityBuilder.HasKey(x => x.riskTreatmentMethodId);
            entityBuilder.Property(x => x.riskTreatmentMethodId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
        }
    }
}


