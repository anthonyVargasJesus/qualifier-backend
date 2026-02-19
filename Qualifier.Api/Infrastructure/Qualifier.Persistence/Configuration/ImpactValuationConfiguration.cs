using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ImpactValuationConfiguration
    {
        public ImpactValuationConfiguration(EntityTypeBuilder<ImpactValuationEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ImpactValuation");
            //entityBuilder.HasKey(x => x.impactValuationId);
            //entityBuilder.Property(x => x.impactValuationId).IsRequired();
            //entityBuilder.Property(x => x.abbreviation).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.defaultValue).IsRequired();
            entityBuilder.ToTable("MAE_IMPACT_VALUATION");

            entityBuilder.HasKey(e => e.impactValuationId)
                .HasName("CST_MAE_IMPACT_VALUATION_PK");

            entityBuilder.Property(e => e.impactValuationId)
                .HasColumnName("N_IMPACT_VALUATION_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.abbreviation)
                .HasColumnName("C_ABBREVIATION")
                .IsRequired();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .IsRequired();

            entityBuilder.Property(e => e.minimumValue)
                .HasColumnName("N_MINIMUM_VALUE");

            entityBuilder.Property(e => e.maximumValue)
                .HasColumnName("N_MAXIMUM_VALUE");

            entityBuilder.Property(e => e.defaultValue)
                .HasColumnName("N_DEFAULT_VALUE")
                .IsRequired();

            entityBuilder.Property(e => e.companyId)
                .HasColumnName("N_COMPANY_ID_FK");

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


