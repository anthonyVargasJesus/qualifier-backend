using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ConfidentialityLevelConfiguration
    {
        public ConfidentialityLevelConfiguration(EntityTypeBuilder<ConfidentialityLevelEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ConfidentialityLevel");
            //entityBuilder.HasKey(x => x.confidentialityLevelId);
            //entityBuilder.Property(x => x.confidentialityLevelId).IsRequired();
            entityBuilder.ToTable("MAE_CONFIDENTIALITY_LEVEL");

            entityBuilder.HasKey(e => e.confidentialityLevelId)
                .HasName("CST_MAE_CONFIDENTIALITY_LEVEL_PK");

            entityBuilder.Property(e => e.confidentialityLevelId)
                .HasColumnName("N_CONFIDENTIALITY_LEVEL_ID_PK");

            entityBuilder.Property(e => e.name)
                .HasColumnName("S_NAME")
                .IsRequired();

            //entityBuilder.Property(e => e.description)
            //    .HasColumnName("S_DESCRIPTION")
            //    .HasMaxLength(500);

            entityBuilder.Property(e => e.companyId)
                .HasColumnName("N_COMPANY_ID_FK");

            entityBuilder.Property(e => e.creationDate)
                .HasColumnName("D_CREATION_DATE");

            entityBuilder.Property(e => e.updateDate)
                .HasColumnName("D_UPDATE_DATE");

            entityBuilder.Property(e => e.creationUserId)
                .HasColumnName("N_CREATION_USER_ID_FK");

            entityBuilder.Property(e => e.updateUserId)
                .HasColumnName("N_UPDATE_USER_ID_FK");

            entityBuilder.Property(e => e.isDeleted)
                .HasColumnName("B_IS_DELETED");
        }
    }
}


