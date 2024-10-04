using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class CreatorConfiguration
    {
        public CreatorConfiguration(EntityTypeBuilder<CreatorEntity> entityBuilder)
        {
            entityBuilder.ToTable("Creator");
            entityBuilder.HasKey(x => x.creatorId);
            entityBuilder.Property(x => x.creatorId).IsRequired();
            entityBuilder.Property(x => x.versionId).IsRequired();
        }
    }
}


