using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class BreachConfiguration
    {
        public BreachConfiguration(EntityTypeBuilder<BreachEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Breach");
            //entityBuilder.HasKey(x => x.breachId);
            //entityBuilder.Property(x => x.breachId).IsRequired();
            //entityBuilder.Property(x => x.evaluationId).IsRequired();
            //entityBuilder.Property(x => x.standardId).IsRequired();
            //entityBuilder.Property(x => x.requirementId).IsRequired();
            //entityBuilder.Property(x => x.controlId).IsRequired();
            //entityBuilder.Property(x => x.description).IsRequired();
            //entityBuilder.Property(x => x.breachSeverityId).IsRequired();
            //entityBuilder.Property(x => x.breachStatusId).IsRequired();
            //entityBuilder.Property(x => x.responsibleId).IsRequired();
            entityBuilder.ToTable("MAE_BREACH");

            entityBuilder.HasKey(e => e.breachId)
                         .HasName("CST_MAE_BREACH_PK");

            entityBuilder.Property(e => e.breachId)
                         .HasColumnName("N_BREACH_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.evaluationId)
                         .HasColumnName("N_EVALUATION_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.standardId)
                         .HasColumnName("N_STANDARD_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.type)
                         .HasColumnName("C_TYPE");

            entityBuilder.Property(e => e.requirementId)
                         .HasColumnName("N_REQUIREMENT_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.controlId)
                         .HasColumnName("N_CONTROL_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.title)
                         .HasColumnName("C_TITLE")
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION")
                         .IsRequired();

            entityBuilder.Property(e => e.breachSeverityId)
                         .HasColumnName("N_BREACH_SEVERITY_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.breachStatusId)
                         .HasColumnName("N_BREACH_STATUS_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.responsibleId)
                         .HasColumnName("N_RESPONSIBLE_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.evidenceDescription)
                         .HasColumnName("C_EVIDENCE_DESCRIPTION");

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

            entityBuilder.Property(e => e.numerationToShow)
                         .HasColumnName("C_NUMERATION_TO_SHOW");
        }
    }
}
