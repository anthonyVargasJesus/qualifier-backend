using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class DocumentationConfiguration
    {
        public DocumentationConfiguration(EntityTypeBuilder<DocumentationEntity> entityBuilder)
        {
            entityBuilder.ToTable("Documentation");
            entityBuilder.HasKey(x => x.documentationId);
            entityBuilder.Property(x => x.documentationId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.standardId).IsRequired();
            entityBuilder.Property(x => x.companyId).IsRequired();

            //entityBuilder.HasOne(x => x.Requirement)
            //.WithMany(x => x.Documentation)
            //.HasForeignKey(x => x.requirementId);
        }
    }
}


