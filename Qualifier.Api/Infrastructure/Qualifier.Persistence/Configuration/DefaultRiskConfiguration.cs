using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class DefaultRiskConfiguration
    {
        public DefaultRiskConfiguration(EntityTypeBuilder<DefaultRiskEntity> entityBuilder)
        {
            //entityBuilder.ToTable("DefaultRisk");
            //entityBuilder.HasKey(x => x.defaultRiskId);
            //entityBuilder.Property(x => x.defaultRiskId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.menaceId).IsRequired();
            //entityBuilder.Property(x => x.vulnerabilityId).IsRequired();

            entityBuilder.ToTable("MAE_DEFAULT_RISK");

            entityBuilder.HasKey(e => e.defaultRiskId)
                .HasName("CST_MAE_DEFAULT_RISK_PK");

            entityBuilder.Property(e => e.defaultRiskId)
                .HasColumnName("N_DEFAULT_RISK_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.standardId)
                .HasColumnName("N_STANDARD_ID_FK");

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .IsRequired();

            entityBuilder.Property(e => e.menaceId)
                .HasColumnName("N_MENACE_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.vulnerabilityId)
                .HasColumnName("N_VULNERABILITY_ID_FK")
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


