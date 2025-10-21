using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class BreachSeverityConfiguration
    {
        public BreachSeverityConfiguration(EntityTypeBuilder<BreachSeverityEntity> entityBuilder)
        {
            entityBuilder.ToTable("BreachSeverity");
            entityBuilder.HasKey(x => x.breachSeverityId);
            entityBuilder.Property(x => x.breachSeverityId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();
        }
    }
}


