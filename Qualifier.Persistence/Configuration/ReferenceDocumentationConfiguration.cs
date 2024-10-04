using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ReferenceDocumentationConfiguration
    {
        public ReferenceDocumentationConfiguration(EntityTypeBuilder<ReferenceDocumentationEntity> entityBuilder)
        {
            entityBuilder.ToTable("ReferenceDocumentation");
            entityBuilder.HasKey(x => x.referenceDocumentationId);
            entityBuilder.Property(x => x.referenceDocumentationId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.documentationId).IsRequired();
        }
    }
}


