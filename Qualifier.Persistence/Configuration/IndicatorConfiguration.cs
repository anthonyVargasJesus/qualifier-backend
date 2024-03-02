using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class IndicatorConfiguration
    {
        public IndicatorConfiguration(EntityTypeBuilder<IndicatorEntity> entityBuilder)
        {
            entityBuilder.ToTable("Indicator");
            entityBuilder.HasKey(x => x.indicatorId);
            entityBuilder.Property(x => x.indicatorId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();
        }
    }
}


