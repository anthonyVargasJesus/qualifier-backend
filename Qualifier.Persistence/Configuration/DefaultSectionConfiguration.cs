using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class DefaultSectionConfiguration
    {
        public DefaultSectionConfiguration(EntityTypeBuilder<DefaultSectionEntity> entityBuilder)
        {
            entityBuilder.ToTable("DefaultSection");
            entityBuilder.HasKey(x => x.defaultSectionId);
            entityBuilder.Property(x => x.defaultSectionId).IsRequired();
            entityBuilder.Property(x => x.numeration).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.level).IsRequired();
            entityBuilder.Property(x => x.documentTypeId).IsRequired();

            ////entityBuilder.HasOne(x => x.defaultSection)
            ////.WithMany(x => x.defaultSections)
            ////.HasForeignKey(x => x.parentId);

            ////entityBuilder.HasOne(x => x.documentType)
            ////.WithMany(x => x.defaultSections)
            ////.HasForeignKey(x => x.documentTypeId);
        }
    }
}


