using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
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

            //entityBuilder.HasOne(x => x.activesInventory)
            //.WithMany(x => x.valuationInActives)
            //.HasForeignKey(x => x.activesInventoryId);

            //entityBuilder.HasOne(x => x.impactValuation)
            //.WithMany(x => x.valuationInActives)
            //.HasForeignKey(x => x.impactValuationId);

            //entityBuilder.HasOne(x => x.company)
            //.WithMany(x => x.valuationInActives)
            //.HasForeignKey(x => x.companyId);
        }
    }
}


