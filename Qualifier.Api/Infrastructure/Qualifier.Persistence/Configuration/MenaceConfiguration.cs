using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MenaceConfiguration
    {
        public MenaceConfiguration(EntityTypeBuilder<MenaceEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Menace");
            //entityBuilder.HasKey(x => x.menaceId);
            //entityBuilder.Property(x => x.menaceId).IsRequired();
            //entityBuilder.Property(x => x.menaceTypeId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.ToTable("MAE_MENACE");

            entityBuilder.HasKey(e => e.menaceId)
                .HasName("CST_MAE_MENACE_PK");

            entityBuilder.Property(e => e.menaceId)
                .HasColumnName("N_MENACE_ID_PK")
                .ValueGeneratedOnAdd();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(e => e.menaceTypeId)
                .HasColumnName("N_MENACE_TYPE_ID_FK");

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
                .HasColumnName("N_IS_DELETED");
        }
    }
}


