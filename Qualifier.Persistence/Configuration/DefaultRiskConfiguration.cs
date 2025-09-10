using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class DefaultRiskConfiguration
    {
        public DefaultRiskConfiguration(EntityTypeBuilder<DefaultRiskEntity> entityBuilder)
        {
            entityBuilder.ToTable("DefaultRisk");
            entityBuilder.HasKey(x => x.defaultRiskId);
            entityBuilder.Property(x => x.defaultRiskId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.menaceId).IsRequired();
            entityBuilder.Property(x => x.vulnerabilityId).IsRequired();
        }
    }
}


