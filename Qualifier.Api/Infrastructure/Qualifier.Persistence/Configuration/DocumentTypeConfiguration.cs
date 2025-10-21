using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class DocumentTypeConfiguration
    {
        public DocumentTypeConfiguration(EntityTypeBuilder<DocumentTypeEntity> entityBuilder)
        {
            entityBuilder.ToTable("DocumentType");
            entityBuilder.HasKey(x => x.documentTypeId);
            entityBuilder.Property(x => x.documentTypeId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
        }
    }
}


