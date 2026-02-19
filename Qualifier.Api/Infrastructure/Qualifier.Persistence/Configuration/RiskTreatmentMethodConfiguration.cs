using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RiskTreatmentMethodConfiguration
    {
        public RiskTreatmentMethodConfiguration(EntityTypeBuilder<RiskTreatmentMethodEntity> entityBuilder)
        {
            //entityBuilder.ToTable("RiskTreatmentMethod");
            //entityBuilder.HasKey(x => x.riskTreatmentMethodId);
            //entityBuilder.Property(x => x.riskTreatmentMethodId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.ToTable("MAE_RISK_TREATMENT_METHOD");

            entityBuilder.HasKey(e => e.riskTreatmentMethodId)
                .HasName("CST_MAE_RISK_TREATMENT_METHOD_PK");

            entityBuilder.Property(e => e.riskTreatmentMethodId)
                .HasColumnName("N_RISK_TREATMENT_METHOD_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .IsRequired();

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

            entityBuilder.Property(e => e.description)
                .HasColumnName("C_DESCRIPTION");
        }
    }
}


