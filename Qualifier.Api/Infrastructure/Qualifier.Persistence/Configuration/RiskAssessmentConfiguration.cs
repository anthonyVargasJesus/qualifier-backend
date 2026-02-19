using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RiskAssessmentConfiguration
    {
        public RiskAssessmentConfiguration(EntityTypeBuilder<RiskAssessmentEntity> entityBuilder)
        {
            //entityBuilder.ToTable("RiskAssessment");
            //entityBuilder.HasKey(x => x.riskAssessmentId);
            //entityBuilder.Property(x => x.riskAssessmentId).IsRequired();
            //entityBuilder.Property(x => x.riskId).IsRequired();
            //entityBuilder.Property(x => x.valuationCID).IsRequired();
            //entityBuilder.Property(x => x.menaceLevelValue).IsRequired();
            //entityBuilder.Property(x => x.vulnerabilityLevelValue).IsRequired();
            //entityBuilder.Property(x => x.existingImplementedControls).IsRequired();
            //entityBuilder.Property(x => x.riskAssessmentValue).IsRequired();
            //entityBuilder.Property(x => x.riskLevelId).IsRequired();
            entityBuilder.ToTable("MAE_RISK_ASSESSMENT");

            entityBuilder.HasKey(e => e.riskAssessmentId)
                         .HasName("CST_MAE_RISK_ASSESSMENT_PK");

            entityBuilder.Property(e => e.riskAssessmentId)
                         .HasColumnName("N_RISK_ASSESSMENT_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.riskId)
                         .HasColumnName("N_RISK_ID")
                         .IsRequired();

            entityBuilder.Property(e => e.valuationCID)
                         .HasColumnName("N_VALUATION_CID")
                         .IsRequired();

            entityBuilder.Property(e => e.menaceLevelValue)
                         .HasColumnName("N_MENACE_LEVEL_VALUE")
                         .IsRequired();

            entityBuilder.Property(e => e.vulnerabilityLevelValue)
                         .HasColumnName("N_VULNERABILITY_LEVEL_VALUE")
                         .IsRequired();

            entityBuilder.Property(e => e.existingImplementedControls)
                         .HasColumnName("C_EXISTING_IMPLEMENTED_CONTROLS");

            entityBuilder.Property(e => e.riskAssessmentValue)
                         .HasColumnName("N_RISK_ASSESSMENT_VALUE")
                         .IsRequired();

            entityBuilder.Property(e => e.riskLevelId)
                         .HasColumnName("N_RISK_LEVEL_ID")
                         .IsRequired();

            entityBuilder.Property(e => e.companyId)
                         .HasColumnName("N_COMPANY_ID");

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


