using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ActivesInventoryConfiguration
    {
        public ActivesInventoryConfiguration(EntityTypeBuilder<ActivesInventoryEntity> entityBuilder)
        {
            entityBuilder.ToTable("ActivesInventory");
            entityBuilder.HasKey(x => x.activesInventoryId);
            entityBuilder.Property(x => x.activesInventoryId).IsRequired();
            entityBuilder.Property(x => x.number).IsRequired();
            entityBuilder.Property(x => x.macroprocessId).IsRequired();
            entityBuilder.Property(x => x.subprocessId).IsRequired();
            entityBuilder.Property(x => x.activeTypeId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.ownerId).IsRequired();
            entityBuilder.Property(x => x.custodianId).IsRequired();
            entityBuilder.Property(x => x.usageClassificationId).IsRequired();
            entityBuilder.Property(x => x.supportTypeId).IsRequired();
            entityBuilder.Property(x => x.locationId).IsRequired();

        }
    }
}


