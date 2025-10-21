using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MenaceConfiguration
    {
        public MenaceConfiguration(EntityTypeBuilder<MenaceEntity> entityBuilder)
        {
            entityBuilder.ToTable("Menace");
            entityBuilder.HasKey(x => x.menaceId);
            entityBuilder.Property(x => x.menaceId).IsRequired();
            entityBuilder.Property(x => x.menaceTypeId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();

        }
    }
}


