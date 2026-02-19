using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RiskConfiguration
    {
        public RiskConfiguration(EntityTypeBuilder<RiskEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Risk");
            //entityBuilder.HasKey(x => x.riskId);
            //entityBuilder.Property(x => x.riskId).IsRequired();
            //entityBuilder.Property(x => x.activesInventoryId).IsRequired();
            //entityBuilder.Property(x => x.menaceId).IsRequired();
            //entityBuilder.Property(x => x.vulnerabilityId).IsRequired();
            entityBuilder.ToTable("MAE_RISK");

            entityBuilder.HasKey(e => e.riskId)
                .HasName("CST_MAE_RISK_PK");

            entityBuilder.Property(e => e.riskId)
                .HasColumnName("N_RISK_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.activesInventoryId)
                .HasColumnName("N_ACTIVES_INVENTORY_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.activesInventoryNumber)
                .HasColumnName("C_ACTIVES_INVENTORY_NUMBER");

            entityBuilder.Property(e => e.activesInventoryName)
                .HasColumnName("C_ACTIVES_INVENTORY_NAME");

            entityBuilder.Property(e => e.menaceId)
                .HasColumnName("N_MENACE_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.vulnerabilityId)
                .HasColumnName("N_VULNERABILITY_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.creationDate)
                .HasColumnName("D_CREATION_DATE");

            entityBuilder.Property(e => e.updateDate)
                .HasColumnName("D_UPDATE_DATE");

            entityBuilder.Property(e => e.creationUserId)
                .HasColumnName("N_CREATION_USER_ID");

            entityBuilder.Property(e => e.updateUserId)
                .HasColumnName("N_UPDATE_USER_ID");

            entityBuilder.Property(e => e.isDeleted)
                .HasColumnName("N_IS_DELETED");

            entityBuilder.Property(e => e.companyId)
                .HasColumnName("N_COMPANY_ID_FK");

            entityBuilder.Property(e => e.evaluationId)
                .HasColumnName("N_EVALUATION_ID_FK");

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME");

            entityBuilder.Property(e => e.riskStatusId)
                .HasColumnName("N_RISK_STATUS_ID_FK");

            entityBuilder.Property(e => e.breachId)
                .HasColumnName("N_BREACH_ID_FK");
        }
    }
}


