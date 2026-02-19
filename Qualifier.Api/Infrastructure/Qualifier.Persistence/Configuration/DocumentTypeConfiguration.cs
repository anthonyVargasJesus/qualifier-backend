using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class DocumentTypeConfiguration
    {
        public DocumentTypeConfiguration(EntityTypeBuilder<DocumentTypeEntity> entityBuilder)
        {
            //entityBuilder.ToTable("DocumentType");
            //entityBuilder.HasKey(x => x.documentTypeId);
            //entityBuilder.Property(x => x.documentTypeId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();

            entityBuilder.ToTable("MAE_DOCUMENT_TYPE");

            entityBuilder.HasKey(e => e.documentTypeId)
                         .HasName("CST_MAE_DOCUMENT_TYPE_PK");

            entityBuilder.Property(e => e.documentTypeId)
                         .HasColumnName("N_DOCUMENT_TYPE_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .HasMaxLength(100)
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION")
                         .HasMaxLength(500);

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
                         .HasColumnName("N_IS_DELETED");
        }
    }
}


