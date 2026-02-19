using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class SectionConfiguration
    {
        public SectionConfiguration(EntityTypeBuilder<SectionEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Section");
            //entityBuilder.HasKey(x => x.sectionId);
            //entityBuilder.Property(x => x.sectionId).IsRequired();
            //entityBuilder.Property(x => x.numeration).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.level).IsRequired();
            //entityBuilder.Property(x => x.companyId).IsRequired();
            entityBuilder.ToTable("MAE_SECTION");

            entityBuilder.HasKey(e => e.sectionId)
                .HasName("CST_MAE_SECTION_PK");

            entityBuilder.Property(e => e.sectionId)
                .HasColumnName("N_SECTION_ID_PK");

            entityBuilder.Property(e => e.numeration)
                .HasColumnName("N_NUMERATION")
                .IsRequired();

            entityBuilder.Property(e => e.name)
                .HasColumnName("S_NAME")
                .HasMaxLength(500)
                .IsRequired();

            entityBuilder.Property(e => e.description)
                .HasColumnName("S_DESCRIPTION")
                .HasMaxLength(500);

            entityBuilder.Property(e => e.level)
                .HasColumnName("N_LEVEL")
                .IsRequired();

            entityBuilder.Property(e => e.parentId)
                .HasColumnName("N_PARENT_ID_FK");

            entityBuilder.Property(e => e.documentationId)
                .HasColumnName("N_DOCUMENTATION_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.versionId)
                .HasColumnName("N_VERSION_ID_FK");
                //.IsRequired();

            entityBuilder.Property(e => e.companyId)
                .HasColumnName("N_COMPANY_ID_FK");

            entityBuilder.Property(e => e.creationDate)
                .HasColumnName("D_CREATION_DATE");

            entityBuilder.Property(e => e.updateDate)
                .HasColumnName("D_UPDATE_DATE");

            entityBuilder.Property(e => e.creationUserId)
                .HasColumnName("N_CREATION_USER_ID_FK");

            entityBuilder.Property(e => e.updateUserId)
                .HasColumnName("N_UPDATE_USER_ID_FK");

            entityBuilder.Property(e => e.isDeleted)
                .HasColumnName("B_IS_DELETED");
        }
    }
}


