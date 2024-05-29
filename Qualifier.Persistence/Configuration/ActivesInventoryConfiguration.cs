using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
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

            //entityBuilder.HasOne(x => x.macroprocess)
            //.WithMany(x => x.activesInventories)
            //.HasForeignKey(x => x.macroprocessId);

            //entityBuilder.HasOne(x => x.subprocess)
            //.WithMany(x => x.activesInventories)
            //.HasForeignKey(x => x.subprocessId);

            //entityBuilder.HasOne(x => x.activeType)
            //.WithMany(x => x.activesInventories)
            //.HasForeignKey(x => x.activeTypeId);

            //entityBuilder.HasOne(x => x.owner)
            //.WithMany(x => x.activesInventories)
            //.HasForeignKey(x => x.ownerId);

            //entityBuilder.HasOne(x => x.custodian)
            //.WithMany(x => x.activesInventories)
            //.HasForeignKey(x => x.custodianId);

            //entityBuilder.HasOne(x => x.usageClassification)
            //.WithMany(x => x.activesInventories)
            //.HasForeignKey(x => x.usageClassificationId);

            //entityBuilder.HasOne(x => x.supportType)
            //.WithMany(x => x.activesInventories)
            //.HasForeignKey(x => x.supportTypeId);

            //entityBuilder.HasOne(x => x.location)
            //.WithMany(x => x.activesInventories)
            //.HasForeignKey(x => x.locationId);
        }
    }
}


