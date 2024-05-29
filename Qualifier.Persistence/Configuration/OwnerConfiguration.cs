using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class OwnerConfiguration
    {
        public OwnerConfiguration(EntityTypeBuilder<OwnerEntity> entityBuilder)
        {
            entityBuilder.ToTable("Owner");
            entityBuilder.HasKey(x => x.ownerId);
            entityBuilder.Property(x => x.ownerId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
        }
    }
}


