using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ImpactValuationConfiguration
    {
        public ImpactValuationConfiguration(EntityTypeBuilder<ImpactValuationEntity> entityBuilder)
        {
            entityBuilder.ToTable("ImpactValuation");
            entityBuilder.HasKey(x => x.impactValuationId);
            entityBuilder.Property(x => x.impactValuationId).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.defaultValue).IsRequired();
        }
    }
}


