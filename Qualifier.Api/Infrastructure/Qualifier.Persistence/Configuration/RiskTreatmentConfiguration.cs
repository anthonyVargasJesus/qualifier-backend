using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RiskTreatmentConfiguration
    {
        public RiskTreatmentConfiguration(EntityTypeBuilder<RiskTreatmentEntity> entityBuilder)
        {
            //entityBuilder.ToTable("RiskTreatment");
            //entityBuilder.HasKey(x => x.riskTreatmentId);
            //entityBuilder.Property(x => x.riskTreatmentId).IsRequired();
            //entityBuilder.Property(x => x.riskId).IsRequired();
            //entityBuilder.Property(x => x.riskTreatmentMethodId).IsRequired();
            //entityBuilder.Property(x => x.controlType).IsRequired();
            entityBuilder.ToTable("MAE_RISK_TREATMENT");

            entityBuilder.HasKey(e => e.riskTreatmentId)
                .HasName("CST_MAE_RISK_TREATMENT_PK");

            entityBuilder.Property(e => e.riskTreatmentId)
                .HasColumnName("N_RISK_TREATMENT_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.riskId)
                .HasColumnName("N_RISK_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.riskTreatmentMethodId)
                .HasColumnName("N_RISK_TREATMENT_METHOD_ID")
                .IsRequired();

            entityBuilder.Property(e => e.controlType)
                .HasColumnName("C_CONTROL_TYPE")
                .IsRequired();

            entityBuilder.Property(e => e.controlsToImplement)
                .HasColumnName("C_CONTROLS_TO_IMPLEMENT");

            entityBuilder.Property(e => e.menaceLevelValue)
                .HasColumnName("N_MENACE_LEVEL_VALUE");

            entityBuilder.Property(e => e.vulnerabilityLevelValue)
                .HasColumnName("N_VULNERABILITY_LEVEL_VALUE");

            entityBuilder.Property(e => e.riskAssessmentValue)
                .HasColumnName("N_RISK_ASSESSMENT_VALUE");

            entityBuilder.Property(e => e.riskLevelId)
                .HasColumnName("N_RISK_LEVEL_ID_FK");

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

            entityBuilder.Property(e => e.residualRiskId)
                .HasColumnName("N_RESIDUAL_RISK_ID_FK");
        }
    }
}


