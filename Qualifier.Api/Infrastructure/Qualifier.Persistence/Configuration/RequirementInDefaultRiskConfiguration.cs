using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RequirementInDefaultRiskConfiguration
    {
        public RequirementInDefaultRiskConfiguration(EntityTypeBuilder<RequirementInDefaultRiskEntity> entityBuilder)
        {
            //entityBuilder.ToTable("RequirementInDefaultRisk");
            //entityBuilder.HasKey(x => x.requirementInDefaultRiskId);
            //entityBuilder.Property(x => x.requirementInDefaultRiskId).IsRequired();
            //entityBuilder.Property(x => x.defaultRiskId).IsRequired();
            //entityBuilder.Property(x => x.requirementId).IsRequired();
            entityBuilder.ToTable("MAE_REQUIREMENT_IN_DEFAULT_RISK");

            entityBuilder.HasKey(e => e.requirementInDefaultRiskId)
                .HasName("CST_MAE_REQ_IN_DEF_RISK_PK");

            entityBuilder.Property(e => e.requirementInDefaultRiskId)
                .HasColumnName("N_REQUIREMENT_IN_DEFAULT_RISK_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.defaultRiskId)
                .HasColumnName("N_DEFAULT_RISK_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.requirementId)
                .HasColumnName("N_REQUIREMENT_ID_FK")
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


