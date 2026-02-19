using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class DefaultSectionConfiguration
    {
        public DefaultSectionConfiguration(EntityTypeBuilder<DefaultSectionEntity> entityBuilder)
        {
            //entityBuilder.ToTable("DefaultSection");
            //entityBuilder.HasKey(x => x.defaultSectionId);
            //entityBuilder.Property(x => x.defaultSectionId).IsRequired();
            //entityBuilder.Property(x => x.numeration).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.level).IsRequired();
            //entityBuilder.Property(x => x.documentTypeId).IsRequired();
            entityBuilder.ToTable("MAE_DEFAULT_SECTION");

            entityBuilder.HasKey(e => e.defaultSectionId)
                         .HasName("CST_MAE_DEFAULT_SECTION_PK");

            entityBuilder.Property(e => e.defaultSectionId)
                         .HasColumnName("N_DEFAULT_SECTION_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.numeration)
                         .HasColumnName("N_NUMERATION")
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .HasMaxLength(500)
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION")
                         .HasMaxLength(500);

            entityBuilder.Property(e => e.level)
                         .HasColumnName("N_LEVEL")
                         .IsRequired();

            entityBuilder.Property(e => e.parentId)
                         .HasColumnName("N_PARENT_ID");

            entityBuilder.Property(e => e.documentTypeId)
                         .HasColumnName("N_DOCUMENT_TYPE_ID");

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


