using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ActivesInventoryConfiguration
    {
        public ActivesInventoryConfiguration(EntityTypeBuilder<ActivesInventoryEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ActivesInventory");
            //entityBuilder.HasKey(x => x.activesInventoryId);
            //entityBuilder.Property(x => x.activesInventoryId).IsRequired();
            //entityBuilder.Property(x => x.number).IsRequired();
            //entityBuilder.Property(x => x.macroprocessId).IsRequired();
            //entityBuilder.Property(x => x.subprocessId).IsRequired();
            //entityBuilder.Property(x => x.activeTypeId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.ownerId).IsRequired();
            //entityBuilder.Property(x => x.custodianId).IsRequired();
            //entityBuilder.Property(x => x.usageClassificationId).IsRequired();
            //entityBuilder.Property(x => x.supportTypeId).IsRequired();
            //entityBuilder.Property(x => x.locationId).IsRequired();
            entityBuilder.ToTable("MAE_ACTIVES_INVENTORY");

            entityBuilder.HasKey(e => e.activesInventoryId);
            entityBuilder.Property(e => e.activesInventoryId)
                .HasColumnName("N_ACTIVES_INVENTORY_ID_PK")
                .IsRequired();

            entityBuilder.Property(e => e.number)
                .HasColumnName("C_NUMBER")
                .IsRequired();

            entityBuilder.Property(e => e.macroprocessId)
                .HasColumnName("N_MACROPROCESS_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.subprocessId)
                .HasColumnName("N_SUBPROCESS_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.procedure)
                .HasColumnName("C_PROCEDURE");

            entityBuilder.Property(e => e.activeTypeId)
                .HasColumnName("N_ACTIVE_TYPE_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .IsRequired();

            entityBuilder.Property(e => e.description)
                .HasColumnName("C_DESCRIPTION");

            entityBuilder.Property(e => e.ownerId)
                .HasColumnName("N_OWNER_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.custodianId)
                .HasColumnName("N_CUSTODIAN_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.usageClassificationId)
                .HasColumnName("N_USAGE_CLASSIFICATION_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.supportTypeId)
                .HasColumnName("N_SUPPORT_TYPE_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.locationId)
                .HasColumnName("N_LOCATION_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.valuation)
                .HasColumnName("N_VALUATION");

            entityBuilder.Property(e => e.companyId)
                .HasColumnName("N_COMPANY_ID_FK");

            entityBuilder.Property(e => e.creationDate)
                .HasColumnName("D_CREATION_DATE");

            entityBuilder.Property(e => e.updateDate)
                .HasColumnName("D_UPDATE_DATE");

            entityBuilder.Property(e => e.creationUserId)
                .HasColumnName("N_CREATION_USER_ID");

            entityBuilder.Property(e => e.updateUserId)
                .HasColumnName("N_UPDATE_USER_ID");

            entityBuilder.Property(e => e.isDeleted)
                .HasColumnName("L_IS_DELETED");

            entityBuilder.Property(e => e.standardId)
                .HasColumnName("N_STANDARD_ID_FK");
        }
    }
}


