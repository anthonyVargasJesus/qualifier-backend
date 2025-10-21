using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ResidualRiskConfiguration
    {
        public ResidualRiskConfiguration(EntityTypeBuilder<ResidualRiskEntity> entityBuilder)
        {
            entityBuilder.ToTable("ResidualRisk");
            entityBuilder.HasKey(x => x.residualRiskId);
            entityBuilder.Property(x => x.residualRiskId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();

  
        }
    }
}


