using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RiskStatusConfiguration
    {
        public RiskStatusConfiguration(EntityTypeBuilder<RiskStatusEntity> entityBuilder)
        {
            entityBuilder.ToTable("RiskStatus");
            entityBuilder.HasKey(x => x.riskStatusId);
            entityBuilder.Property(x => x.riskStatusId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();
        }
    }
}


