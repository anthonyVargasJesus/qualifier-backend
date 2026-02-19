using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ControlGroupConfiguration
    {
        public ControlGroupConfiguration(EntityTypeBuilder<ControlGroupEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ControlGroup");
            //entityBuilder.HasKey(x => x.controlGroupId);
            //entityBuilder.Property(x => x.controlGroupId).IsRequired();
            //entityBuilder.Property(x => x.number).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();

            entityBuilder.ToTable("MAE_CONTROL_GROUP");

            entityBuilder.HasKey(e => e.controlGroupId)
                         .HasName("CST_MAE_CONTROL_GROUP_PK");

            entityBuilder.Property(e => e.controlGroupId)
                         .HasColumnName("N_CONTROL_GROUP_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.number)
                         .HasColumnName("N_NUMBER")
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .HasMaxLength(100)
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION")
                         .HasMaxLength(500);

            entityBuilder.Property(e => e.standardId)
                         .HasColumnName("N_STANDARD_ID");

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


