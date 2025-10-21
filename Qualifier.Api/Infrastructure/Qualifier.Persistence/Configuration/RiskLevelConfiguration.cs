using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RiskLevelConfiguration
    {
        public RiskLevelConfiguration(EntityTypeBuilder<RiskLevelEntity> entityBuilder)
        {
            entityBuilder.ToTable("RiskLevel");
            entityBuilder.HasKey(x => x.riskLevelId);
            entityBuilder.Property(x => x.riskLevelId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.minimum).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();
        }
    }
}


