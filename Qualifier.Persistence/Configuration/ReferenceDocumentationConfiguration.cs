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

            entityBuilder.HasOne(x => x.documentation)
            .WithMany(x => x.referenceDocumentations)
            .HasForeignKey(x => x.documentationId);

            //entityBuilder.HasOne(x => x.requirementEvaluation)
            //.WithMany(x => x.)
            //.HasForeignKey(x => x.requirementEvaluationId);

            //entityBuilder.HasOne(x => x.controlEvaluation)
            //.WithMany(x => x.)
            //.HasForeignKey(x => x.controlEvaluationId);
        }
    }
}


