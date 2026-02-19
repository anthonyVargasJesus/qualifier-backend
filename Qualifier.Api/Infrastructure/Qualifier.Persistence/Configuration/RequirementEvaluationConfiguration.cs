using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RequirementEvaluationConfiguration
    {
        public RequirementEvaluationConfiguration(EntityTypeBuilder<RequirementEvaluationEntity> entityBuilder)
        {
            //entityBuilder.ToTable("RequirementEvaluation");
            //entityBuilder.HasKey(x => x.requirementEvaluationId);
            //entityBuilder.Property(x => x.requirementEvaluationId).IsRequired();
            //entityBuilder.Property(x => x.evaluationId).IsRequired();
            //entityBuilder.Property(x => x.requirementId).IsRequired();
            //entityBuilder.Property(x => x.maturityLevelId).IsRequired();
            //entityBuilder.Property(x => x.value).IsRequired();
            //entityBuilder.Property(x => x.responsibleId).IsRequired();
            //entityBuilder.Property(x => x.justification).IsRequired();
            //entityBuilder.Property(x => x.improvementActions).IsRequired();
            //entityBuilder.Property(x => x.companyId).IsRequired();

            //entityBuilder.HasOne(x => x.evaluation)
            //.WithMany(x => x.requirementEvaluations)
            //.HasForeignKey(x => x.evaluationId);

            //entityBuilder.HasOne(x => x.requirement)
            //.WithMany(x => x.requirementEvaluations)
            //.HasForeignKey(x => x.requirementId);

            //entityBuilder.HasOne(x => x.maturityLevel)
            //.WithMany(x => x.requirementEvaluations)
            //.HasForeignKey(x => x.maturityLevelId);

            //entityBuilder.HasOne(x => x.responsible)
            //.WithMany(x => x.requirementEvaluations)
            //.HasForeignKey(x => x.responsibleId);

            entityBuilder.ToTable("MAE_REQUIREMENT_EVALUATION");

            entityBuilder.HasKey(e => e.requirementEvaluationId)
                         .HasName("CST_MAE_REQUIREMENT_EVALUATION_PK");

            entityBuilder.Property(e => e.requirementEvaluationId)
                         .HasColumnName("N_REQUIREMENT_EVALUATION_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.evaluationId)
                         .HasColumnName("N_EVALUATION_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.requirementId)
                         .HasColumnName("N_REQUIREMENT_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.maturityLevelId)
                         .HasColumnName("N_MATURITY_LEVEL_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.value)
                         .HasColumnName("N_VALUE")
                         .IsRequired();

            entityBuilder.Property(e => e.responsibleId)
                         .HasColumnName("N_RESPONSIBLE_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.justification)
                         .HasColumnName("C_JUSTIFICATION");

            entityBuilder.Property(e => e.improvementActions)
                         .HasColumnName("C_IMPROVEMENT_ACTIONS");

            entityBuilder.Property(e => e.standardId)
                         .HasColumnName("N_STANDARD_ID_FK");

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

            entityBuilder.Property(e => e.auditorStatus)
                         .HasColumnName("N_AUDITOR_STATUS");

        }
    }
}
