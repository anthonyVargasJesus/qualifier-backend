using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class VersionConfiguration
    {
        public VersionConfiguration(EntityTypeBuilder<VersionEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Version");
            //entityBuilder.HasKey(x => x.versionId);
            //entityBuilder.Property(x => x.versionId).IsRequired();
            //entityBuilder.Property(x => x.number).IsRequired();
            //entityBuilder.Property(x => x.code).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.confidentialityLevelId).IsRequired();
            //entityBuilder.Property(x => x.documentationId).IsRequired();
            //entityBuilder.Property(x => x.date).IsRequired();
            //entityBuilder.Property(x => x.isCurrent).IsRequired();
            //entityBuilder.Property(x => x.standardId).IsRequired();

            entityBuilder.ToTable("MAE_VERSION");

            entityBuilder.HasKey(e => e.versionId)
                .HasName("CST_MAE_VERSION_PK");

            entityBuilder.Property(e => e.versionId)
                .HasColumnName("N_VERSION_ID_PK");

            entityBuilder.Property(e => e.code)
                .HasColumnName("S_CODE")
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(e => e.name)
                .HasColumnName("S_NAME")
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(e => e.description)
                .HasColumnName("S_DESCRIPTION")
                .HasMaxLength(500);

            entityBuilder.Property(e => e.number)
                .HasColumnName("N_NUMBER")
                .HasPrecision(18, 2)
                .IsRequired();

            entityBuilder.Property(e => e.date)
                .HasColumnName("D_DATE")
                .IsRequired();

            entityBuilder.Property(e => e.fileName)
                .HasColumnName("S_FILE_NAME")
                .HasMaxLength(500);

            entityBuilder.Property(e => e.documentationId)
                .HasColumnName("N_DOCUMENTATION_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.confidentialityLevelId)
                .HasColumnName("N_CONFIDENTIALITY_LEVEL_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.isCurrent)
                .HasColumnName("B_IS_CURRENT")
                .IsRequired();

            entityBuilder.Property(e => e.standardId)
                .HasColumnName("N_STANDARD_ID_FK")
                .IsRequired();

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

            entityBuilder.Property(e => e.versionReferenceId)
                .HasColumnName("N_VERSION_REFERENCE_ID_FK");

        }
    }
}


