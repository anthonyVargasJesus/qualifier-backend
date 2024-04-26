using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class SectionConfiguration
    {
        public SectionConfiguration(EntityTypeBuilder<SectionEntity> entityBuilder)
        {
            entityBuilder.ToTable("Section");
            entityBuilder.HasKey(x => x.sectionId);
            entityBuilder.Property(x => x.sectionId).IsRequired();
            entityBuilder.Property(x => x.numeration).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.level).IsRequired();
            entityBuilder.Property(x => x.companyId).IsRequired();

            //entityBuilder.HasOne(x => x.section)
            //.WithMany(x => x.sections)
            //.HasForeignKey(x => x.parentId);

            //entityBuilder.HasOne(x => x.documentation)
            //.WithMany(x => x.sections)
            //.HasForeignKey(x => x.documentationId);

            //entityBuilder.HasOne(x => x.version)
            //.WithMany(x => x.sections)
            //.HasForeignKey(x => x.versionId);
        }
    }
}


