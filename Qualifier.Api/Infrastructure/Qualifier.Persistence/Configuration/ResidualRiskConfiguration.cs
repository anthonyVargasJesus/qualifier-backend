using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ResidualRiskConfiguration
    {
        public ResidualRiskConfiguration(EntityTypeBuilder<ResidualRiskEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ResidualRisk");
            //entityBuilder.HasKey(x => x.residualRiskId);
            //entityBuilder.Property(x => x.residualRiskId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.abbreviation).IsRequired();
            //entityBuilder.Property(x => x.color).IsRequired();
            entityBuilder.ToTable("MAE_RESIDUAL_RISK");

            entityBuilder.HasKey(e => e.residualRiskId)
                .HasName("CST_MAE_RESIDUAL_RISK_PK");

            entityBuilder.Property(e => e.residualRiskId)
                .HasColumnName("N_RESIDUAL_RISK_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .IsRequired();

            entityBuilder.Property(e => e.description)
                .HasColumnName("C_DESCRIPTION");

            entityBuilder.Property(e => e.abbreviation)
                .HasColumnName("C_ABBREVIATION")
                .IsRequired();

            entityBuilder.Property(e => e.factor)
                .HasColumnName("N_FACTOR");

            entityBuilder.Property(e => e.minimum)
                .HasColumnName("N_MINIMUM");

            entityBuilder.Property(e => e.maximum)
                .HasColumnName("N_MAXIMUM");

            entityBuilder.Property(e => e.color)
                .HasColumnName("C_COLOR")
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

        }
    }
}


