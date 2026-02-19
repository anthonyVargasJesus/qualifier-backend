using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ReferenceDocumentationConfiguration
    {
        public ReferenceDocumentationConfiguration(EntityTypeBuilder<ReferenceDocumentationEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ReferenceDocumentation");
            //entityBuilder.HasKey(x => x.referenceDocumentationId);
            //entityBuilder.Property(x => x.referenceDocumentationId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.documentationId).IsRequired();
            entityBuilder.ToTable("MAE_REFERENCE_DOCUMENTATION");

            entityBuilder.HasKey(e => e.referenceDocumentationId)
                         .HasName("CST_MAE_REFERENCE_DOCUMENTATION_PK");

            entityBuilder.Property(e => e.referenceDocumentationId)
                         .HasColumnName("N_REFERENCE_DOCUMENTATION_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION");

            entityBuilder.Property(e => e.documentationId)
                         .HasColumnName("N_DOCUMENTATION_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.requirementEvaluationId)
                         .HasColumnName("N_REQUIREMENT_EVALUATION_ID_FK");

            entityBuilder.Property(e => e.controlEvaluationId)
                         .HasColumnName("N_CONTROL_EVALUATION_ID_FK");

            entityBuilder.Property(e => e.evaluationId)
                         .HasColumnName("N_EVALUATION_ID_FK");

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

            entityBuilder.Property(e => e.url)
                         .HasColumnName("C_URL");
        }
    }
}


