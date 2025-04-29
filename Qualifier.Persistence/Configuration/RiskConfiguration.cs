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

        }
    }
}


