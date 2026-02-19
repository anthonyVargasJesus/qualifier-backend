using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class SupportTypeConfiguration
    {
        public SupportTypeConfiguration(EntityTypeBuilder<SupportTypeEntity> entityBuilder)
        {
            //entityBuilder.ToTable("SupportType");
            //entityBuilder.HasKey(x => x.supportTypeId);
            //entityBuilder.Property(x => x.supportTypeId).IsRequired();
            entityBuilder.ToTable("MAE_SUPPORT_TYPE");

            entityBuilder.HasKey(e => e.supportTypeId)
                .HasName("CST_MAE_SUPPORT_TYPE_PK");

            entityBuilder.Property(e => e.supportTypeId)
                .HasColumnName("N_SUPPORT_TYPE_ID_PK")
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
        }
    }
}


