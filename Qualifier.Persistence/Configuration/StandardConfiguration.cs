using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class StandardConfiguration
    {
        public StandardConfiguration(EntityTypeBuilder<StandardEntity> entityBuilder)
        {
            entityBuilder.ToTable("Standard");
            entityBuilder.HasKey(x => x.standardId);
            entityBuilder.Property(x => x.standardId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();

            entityBuilder.HasOne(x => x.standard)
            .WithMany(x => x.standards)
            .HasForeignKey(x => x.parentId);

        }
    }
}


