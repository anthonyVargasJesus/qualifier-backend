using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ValuationInActiveConfiguration
    {
        public ValuationInActiveConfiguration(EntityTypeBuilder<ValuationInActiveEntity> entityBuilder)
        {
            entityBuilder.ToTable("ValuationInActive");
            entityBuilder.HasKey(x => x.valuationInActiveId);
            entityBuilder.Property(x => x.valuationInActiveId).IsRequired();
            entityBuilder.Property(x => x.activesInventoryId).IsRequired();
            entityBuilder.Property(x => x.impactValuationId).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
        }
    }
}


