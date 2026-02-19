using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class LocationConfiguration
    {
        public LocationConfiguration(EntityTypeBuilder<LocationEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Location");
            //entityBuilder.HasKey(x => x.locationId);
            //entityBuilder.Property(x => x.locationId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.ToTable("MAE_LOCATION");

            entityBuilder.HasKey(e => e.locationId)
                .HasName("CST_MAE_LOCATION_PK");

            entityBuilder.Property(e => e.locationId)
                .HasColumnName("N_LOCATION_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.abbreviation)
                .HasColumnName("C_ABBREVIATION")
                .IsRequired();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
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
                .HasColumnName("N_IS_DELETED");
        }
    }
}


