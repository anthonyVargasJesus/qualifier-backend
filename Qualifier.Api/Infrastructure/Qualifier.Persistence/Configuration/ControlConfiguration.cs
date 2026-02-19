using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ControlConfiguration
    {
        public ControlConfiguration(EntityTypeBuilder<ControlEntity> entityBuilder)
        {
            entityBuilder.ToTable("MAE_CONTROL");

            entityBuilder.HasKey(e => e.controlId)
                         .HasName("CST_MAE_CONTROL_PK");

            entityBuilder.Property(e => e.controlId)
                         .HasColumnName("N_CONTROL_ID_PK")
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

            entityBuilder.Property(e => e.controlGroupId)
                         .HasColumnName("N_CONTROL_GROUP_ID");

            entityBuilder.Property(e => e.standardId)
                         .HasColumnName("N_STANDARD_ID")
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
                         .HasColumnName("N_IS_DELETED");

        }
    }
}


