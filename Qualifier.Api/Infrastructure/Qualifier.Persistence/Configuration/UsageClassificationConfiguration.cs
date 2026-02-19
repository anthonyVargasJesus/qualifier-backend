using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class UsageClassificationConfiguration
    {
        public UsageClassificationConfiguration(EntityTypeBuilder<UsageClassificationEntity> entityBuilder)
        {
            //entityBuilder.ToTable("UsageClassification");
            //entityBuilder.HasKey(x => x.usageClassificationId);
            //entityBuilder.Property(x => x.usageClassificationId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.ToTable("MAE_USAGE_CLASSIFICATION");

            entityBuilder.HasKey(e => e.usageClassificationId)
                .HasName("CST_MAE_USAGE_CLASSIFICATION_PK");

            entityBuilder.Property(e => e.usageClassificationId)
                .HasColumnName("N_USAGE_CLASSIFICATION_ID_PK")
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
        }
    }
}


