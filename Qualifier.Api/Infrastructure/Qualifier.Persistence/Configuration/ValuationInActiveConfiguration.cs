using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ValuationInActiveConfiguration
    {
        public ValuationInActiveConfiguration(EntityTypeBuilder<ValuationInActiveEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ValuationInActive");
            //entityBuilder.HasKey(x => x.valuationInActiveId);
            //entityBuilder.Property(x => x.valuationInActiveId).IsRequired();
            //entityBuilder.Property(x => x.activesInventoryId).IsRequired();
            //entityBuilder.Property(x => x.impactValuationId).IsRequired();
            //entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.ToTable("MAE_VALUATION_IN_ACTIVE");

            entityBuilder.HasKey(e => e.valuationInActiveId)
                         .HasName("CST_MAE_VALUATION_IN_ACTIVE_PK");

            entityBuilder.Property(e => e.valuationInActiveId)
                         .HasColumnName("N_VALUATION_IN_ACTIVE_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.activesInventoryId)
                         .HasColumnName("N_ACTIVES_INVENTORY_ID")
                         .IsRequired();

            entityBuilder.Property(e => e.impactValuationId)
                         .HasColumnName("N_IMPACT_VALUATION_ID")
                         .IsRequired();

            entityBuilder.Property(e => e.value)
                         .HasColumnName("N_VALUE")
                         .IsRequired();

            entityBuilder.Property(e => e.companyId)
                         .HasColumnName("N_COMPANY_ID");

            entityBuilder.Property(e => e.creationDate)
                         .HasColumnName("D_CREATION_DATE");

            entityBuilder.Property(e => e.updateDate)
                         .HasColumnName("D_UPDATE_DATE");

            entityBuilder.Property(e => e.creationUserId)
                         .HasColumnName("N_CREATION_USER_ID");

            entityBuilder.Property(e => e.updateUserId)
                         .HasColumnName("N_UPDATE_USER_ID");

            entityBuilder.Property(e => e.isDeleted)
                         .HasColumnName("L_IS_DELETED");
        }
    }
}


