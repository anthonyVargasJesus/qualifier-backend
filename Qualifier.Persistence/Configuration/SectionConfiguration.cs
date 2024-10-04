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
        }
    }
}


