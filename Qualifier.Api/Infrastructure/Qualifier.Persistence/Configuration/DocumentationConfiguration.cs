using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class DocumentationConfiguration
    {
        public DocumentationConfiguration(EntityTypeBuilder<DocumentationEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Documentation");
            //entityBuilder.HasKey(x => x.documentationId);
            //entityBuilder.Property(x => x.documentationId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.standardId).IsRequired();
            //entityBuilder.Property(x => x.companyId).IsRequired();
            entityBuilder.ToTable("MAE_DOCUMENTATION");

            entityBuilder.HasKey(e => e.documentationId)
                         .HasName("CST_MAE_DOCUMENTATION_PK");

            entityBuilder.Property(e => e.documentationId)
                         .HasColumnName("N_DOCUMENTATION_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .HasMaxLength(100)
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION")
                         .HasMaxLength(500);

            entityBuilder.Property(e => e.template)
                         .HasColumnName("C_TEMPLATE")
                         .HasMaxLength(200);

            entityBuilder.Property(e => e.standardId)
                         .HasColumnName("N_STANDARD_ID");

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

            entityBuilder.Property(e => e.documentTypeId)
                         .HasColumnName("N_DOCUMENT_TYPE_ID");
        }
    }
}


