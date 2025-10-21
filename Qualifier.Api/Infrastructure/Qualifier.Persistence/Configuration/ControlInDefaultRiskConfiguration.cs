using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ControlInDefaultRiskConfiguration
    {
        public ControlInDefaultRiskConfiguration(EntityTypeBuilder<ControlInDefaultRiskEntity> entityBuilder)
        {
            entityBuilder.ToTable("ControlInDefaultRisk");
            entityBuilder.HasKey(x => x.controlInDefaultRiskId);
            entityBuilder.Property(x => x.controlInDefaultRiskId).IsRequired();
            entityBuilder.Property(x => x.defaultRiskId).IsRequired();
            entityBuilder.Property(x => x.controlId).IsRequired();
        }
    }
}

