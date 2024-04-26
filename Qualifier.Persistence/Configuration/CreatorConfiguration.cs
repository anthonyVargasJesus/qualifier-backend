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

            //entityBuilder.HasOne(x => x.personal)
            //.WithMany(x => x.creators)
            //.HasForeignKey(x => x.personalId);

            //entityBuilder.HasOne(x => x.responsible)
            //.WithMany(x => x.creators)
            //.HasForeignKey(x => x.responsibleId);

            //entityBuilder.HasOne(x => x.version)
            //.WithMany(x => x.creators)
            //.HasForeignKey(x => x.versionId);
        }
    }
}


