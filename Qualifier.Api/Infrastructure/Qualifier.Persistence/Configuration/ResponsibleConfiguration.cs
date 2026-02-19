using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ResponsibleConfiguration
    {
        public ResponsibleConfiguration(EntityTypeBuilder<ResponsibleEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Responsible");
            //entityBuilder.HasKey(x => x.responsibleId);
            //entityBuilder.Property(x => x.responsibleId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.companyId).IsRequired();
            entityBuilder.ToTable("MAE_RESPONSIBLE");

            entityBuilder.HasKey(e => e.responsibleId)
                         .HasName("CST_MAE_RESPONSIBLE_PK");

            entityBuilder.Property(e => e.responsibleId)
                         .HasColumnName("N_RESPONSIBLE_ID_PK")
                         .ValueGeneratedOnAdd()
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


