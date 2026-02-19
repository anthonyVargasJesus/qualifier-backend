using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ControlTypeConfiguration
    {
        public ControlTypeConfiguration(EntityTypeBuilder<ControlTypeEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ControlType");
            //entityBuilder.HasKey(x => x.controlTypeId);
            //entityBuilder.Property(x => x.controlTypeId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.description).IsRequired();
            //entityBuilder.Property(x => x.abbreviation).IsRequired();
            //entityBuilder.Property(x => x.minimum).IsRequired();
            //entityBuilder.Property(x => x.color).IsRequired();

            entityBuilder.ToTable("MAE_CONTROL_TYPE");

            entityBuilder.HasKey(e => e.controlTypeId);
            entityBuilder.Property(e => e.controlTypeId)
                .HasColumnName("N_CONTROL_TYPE_ID_PK")
                .ValueGeneratedOnAdd();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(e => e.description)
                .HasColumnName("C_DESCRIPTION")
                .HasMaxLength(500)
                .IsRequired();

            entityBuilder.Property(e => e.abbreviation)
                .HasColumnName("C_ABBREVIATION")
                .HasMaxLength(10)
                .IsRequired();

            entityBuilder.Property(e => e.factor)
                .HasColumnName("N_FACTOR")
                .HasColumnType("numeric(18,2)");

            entityBuilder.Property(e => e.minimum)
                .HasColumnName("N_MINIMUM")
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            entityBuilder.Property(e => e.maximum)
                .HasColumnName("N_MAXIMUM")
                .HasColumnType("numeric(18,2)");

            entityBuilder.Property(e => e.color)
                .HasColumnName("C_COLOR")
                .HasMaxLength(100)
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

