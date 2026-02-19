using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ControlEvaluationConfiguration
    {
        public ControlEvaluationConfiguration(EntityTypeBuilder<ControlEvaluationEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ControlEvaluation");
            //entityBuilder.HasKey(x => x.controlEvaluationId);
            //entityBuilder.Property(x => x.controlEvaluationId).IsRequired();
            //entityBuilder.Property(x => x.evaluationId).IsRequired();
            //entityBuilder.Property(x => x.controlId).IsRequired();
            //entityBuilder.Property(x => x.maturityLevelId).IsRequired();
            //entityBuilder.Property(x => x.value).IsRequired();
            //entityBuilder.Property(x => x.responsibleId).IsRequired();
            //entityBuilder.Property(x => x.justification).IsRequired();
            //entityBuilder.Property(x => x.improvementActions).IsRequired();
            //entityBuilder.Property(x => x.controlDescription).IsRequired();
            //entityBuilder.Property(x => x.controlType).IsRequired();
            //entityBuilder.Property(x => x.companyId).IsRequired();

            //entityBuilder.HasOne(x => x.evaluation)
            //.WithMany(x => x.controlEvaluations)
            //.HasForeignKey(x => x.evaluationId);

            //entityBuilder.HasOne(x => x.control)
            //.WithMany(x => x.controlEvaluations)
            //.HasForeignKey(x => x.controlId);

            //entityBuilder.HasOne(x => x.maturityLevel)
            //.WithMany(x => x.controlEvaluations)
            //.HasForeignKey(x => x.maturityLevelId);

            //entityBuilder.HasOne(x => x.responsible)
            //.WithMany(x => x.controlEvaluations)
            //.HasForeignKey(x => x.responsibleId);

            entityBuilder.ToTable("MAE_CONTROL_EVALUATION");

            entityBuilder.HasKey(e => e.controlEvaluationId)
                         .HasName("CST_MAE_CONTROL_EVALUATION_PK");

            entityBuilder.Property(e => e.controlEvaluationId)
                         .HasColumnName("N_CONTROL_EVALUATION_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.evaluationId)
                         .HasColumnName("N_EVALUATION_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.controlId)
                         .HasColumnName("N_CONTROL_ID_FK")
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
                         .HasColumnName("C_JUSTIFICATION")
                         .IsRequired();

            entityBuilder.Property(e => e.improvementActions)
                         .HasColumnName("C_IMPROVEMENT_ACTIONS")
                         .IsRequired();

            entityBuilder.Property(e => e.controlDescription)
                         .HasColumnName("C_CONTROL_DESCRIPTION")
                         .IsRequired();

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

            entityBuilder.Property(e => e.controlType)
                         .HasColumnName("C_CONTROL_TYPE");

        }
    }
}


