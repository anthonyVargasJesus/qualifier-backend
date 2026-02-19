using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ReviewerConfiguration
    {
        public ReviewerConfiguration(EntityTypeBuilder<ReviewerEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Reviewer");
            //entityBuilder.HasKey(x => x.reviewerId);
            //entityBuilder.Property(x => x.reviewerId).IsRequired();
            //entityBuilder.Property(x => x.versionId).IsRequired();
            entityBuilder.ToTable("MAE_REVIEWER");

            entityBuilder.HasKey(e => e.reviewerId)
                         .HasName("CST_MAE_REVIEWER_PK");

            entityBuilder.Property(e => e.reviewerId)
                         .HasColumnName("N_REVIEWER_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.personalId)
                         .HasColumnName("N_PERSONAL_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.responsibleId)
                         .HasColumnName("N_RESPONSIBLE_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.versionId)
                         .HasColumnName("N_VERSION_ID_FK");

            entityBuilder.Property(e => e.documentationId)
                         .HasColumnName("N_DOCUMENTATION_ID_FK");

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
                         .HasColumnName("L_IS_DELETED");
        }
    }
}


