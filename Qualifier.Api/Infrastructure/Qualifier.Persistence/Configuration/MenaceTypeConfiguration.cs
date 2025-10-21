using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MenaceTypeConfiguration
    {
        public MenaceTypeConfiguration(EntityTypeBuilder<MenaceTypeEntity> entityBuilder)
        {
            entityBuilder.ToTable("MenaceType");
            entityBuilder.HasKey(x => x.menaceTypeId);
            entityBuilder.Property(x => x.menaceTypeId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();

        }
    }
}

