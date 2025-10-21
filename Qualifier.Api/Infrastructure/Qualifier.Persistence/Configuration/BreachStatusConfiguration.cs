using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class BreachStatusConfiguration
    {
        public BreachStatusConfiguration(EntityTypeBuilder<BreachStatusEntity> entityBuilder)
        {
            entityBuilder.ToTable("BreachStatus");
            entityBuilder.HasKey(x => x.breachStatusId);
            entityBuilder.Property(x => x.breachStatusId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();
        }
    }
}


