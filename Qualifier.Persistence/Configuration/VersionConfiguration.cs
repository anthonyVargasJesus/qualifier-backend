using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class VersionConfiguration
    {
        public VersionConfiguration(EntityTypeBuilder<VersionEntity> entityBuilder)
        {
            entityBuilder.ToTable("Version");
            entityBuilder.HasKey(x => x.versionId);
            entityBuilder.Property(x => x.versionId).IsRequired();
            entityBuilder.Property(x => x.number).IsRequired();
            entityBuilder.Property(x => x.code).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.confidentialityLevelId).IsRequired();
            entityBuilder.Property(x => x.documentationId).IsRequired();
            entityBuilder.Property(x => x.date).IsRequired();
            entityBuilder.Property(x => x.isCurrent).IsRequired();
            entityBuilder.Property(x => x.standardId).IsRequired();
        }
    }
}


