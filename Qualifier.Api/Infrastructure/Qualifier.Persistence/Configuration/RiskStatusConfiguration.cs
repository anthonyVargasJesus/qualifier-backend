using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RiskStatusConfiguration
    {
        public RiskStatusConfiguration(EntityTypeBuilder<RiskStatusEntity> entityBuilder)
        {
            //entityBuilder.ToTable("RiskStatus");
            //entityBuilder.HasKey(x => x.riskStatusId);
            //entityBuilder.Property(x => x.riskStatusId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.description).IsRequired();
            //entityBuilder.Property(x => x.abbreviation).IsRequired();
            //entityBuilder.Property(x => x.value).IsRequired();
            //entityBuilder.Property(x => x.color).IsRequired();
            entityBuilder.ToTable("MAE_RISK_STATUS");

            entityBuilder.HasKey(e => e.riskStatusId);

            entityBuilder.Property(e => e.riskStatusId)
                .HasColumnName("N_RISK_STATUS_ID_PK")
                .ValueGeneratedOnAdd();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .IsRequired();

            entityBuilder.Property(e => e.description)
                .HasColumnName("C_DESCRIPTION")
                .IsRequired();

            entityBuilder.Property(e => e.abbreviation)
                .HasColumnName("C_ABBREVIATION")
                .IsRequired();

            entityBuilder.Property(e => e.value)
                .HasColumnName("N_VALUE")
                .IsRequired();

            entityBuilder.Property(e => e.color)
                .HasColumnName("C_COLOR")
                .IsRequired();

            entityBuilder.Property(e => e.companyId)
                .HasColumnName("N_COMPANY_ID_FK")
                .IsRequired(false);

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


