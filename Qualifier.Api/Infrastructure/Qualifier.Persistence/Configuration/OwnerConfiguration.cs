using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class OwnerConfiguration
    {
        public OwnerConfiguration(EntityTypeBuilder<OwnerEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Owner");
            //entityBuilder.HasKey(x => x.ownerId);
            //entityBuilder.Property(x => x.ownerId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.ToTable("MAE_OWNER");

            entityBuilder.HasKey(e => e.ownerId)
                .HasName("CST_MAE_OWNER_PK");

            entityBuilder.Property(e => e.ownerId)
                .HasColumnName("N_OWNER_ID_PK")
                .ValueGeneratedOnAdd()
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

            entityBuilder.Property(e => e.code)
                .HasColumnName("C_CODE");
        }
    }
}


