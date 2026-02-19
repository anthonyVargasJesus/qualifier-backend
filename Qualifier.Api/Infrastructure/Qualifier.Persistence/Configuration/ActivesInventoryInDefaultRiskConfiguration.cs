using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ActivesInventoryInDefaultRiskConfiguration
    {
        public ActivesInventoryInDefaultRiskConfiguration(EntityTypeBuilder<ActivesInventoryInDefaultRiskEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ActivesInventoryInDefaultRisk");
            //entityBuilder.HasKey(x => x.activesInventoryInDefaultRiskId);
            //entityBuilder.Property(x => x.activesInventoryInDefaultRiskId).IsRequired();
            //entityBuilder.Property(x => x.defaultRiskId).IsRequired();
            //entityBuilder.Property(x => x.activesInventoryId).IsRequired();
            entityBuilder.ToTable("MAE_ACTIVES_INVENTORY_IN_DEFAULT_RISK");

            entityBuilder.HasKey(e => e.activesInventoryInDefaultRiskId)
                         .HasName("CST_MAE_ACTIVES_INVENTORY_IN_DEFAULT_RISK_PK");

            entityBuilder.Property(e => e.activesInventoryInDefaultRiskId)
                         .HasColumnName("N_ACTIVES_INVENTORY_IN_DEFAULT_RISK_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.defaultRiskId)
                         .HasColumnName("N_DEFAULT_RISK_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.activesInventoryId)
                         .HasColumnName("N_ACTIVES_INVENTORY_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.isActive)
                         .HasColumnName("L_IS_ACTIVE");

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
        }
    }
}


