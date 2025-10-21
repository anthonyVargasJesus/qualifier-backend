using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ActivesInventoryInDefaultRiskConfiguration
    {
        public ActivesInventoryInDefaultRiskConfiguration(EntityTypeBuilder<ActivesInventoryInDefaultRiskEntity> entityBuilder)
        {
            entityBuilder.ToTable("ActivesInventoryInDefaultRisk");
            entityBuilder.HasKey(x => x.activesInventoryInDefaultRiskId);
            entityBuilder.Property(x => x.activesInventoryInDefaultRiskId).IsRequired();
            entityBuilder.Property(x => x.defaultRiskId).IsRequired();
            entityBuilder.Property(x => x.activesInventoryId).IsRequired();
        }
    }
}


