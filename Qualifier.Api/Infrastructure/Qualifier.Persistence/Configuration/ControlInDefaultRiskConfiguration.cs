using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ControlInDefaultRiskConfiguration
    {
        public ControlInDefaultRiskConfiguration(EntityTypeBuilder<ControlInDefaultRiskEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ControlInDefaultRisk");
            //entityBuilder.HasKey(x => x.controlInDefaultRiskId);
            //entityBuilder.Property(x => x.controlInDefaultRiskId).IsRequired();
            //entityBuilder.Property(x => x.defaultRiskId).IsRequired();
            //entityBuilder.Property(x => x.controlId).IsRequired();
            entityBuilder.ToTable("MAE_CONTROL_IN_DEFAULT_RISK");

            entityBuilder.HasKey(e => e.controlInDefaultRiskId)
                .HasName("CST_MAE_CTRL_IN_DEF_RISK_PK");

            entityBuilder.Property(e => e.controlInDefaultRiskId)
                .HasColumnName("N_CONTROL_IN_DEFAULT_RISK_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.defaultRiskId)
                .HasColumnName("N_DEFAULT_RISK_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.controlId)
                .HasColumnName("N_CONTROL_ID_FK")
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
                .HasColumnName("N_IS_DELETED");
        }
    }
}

