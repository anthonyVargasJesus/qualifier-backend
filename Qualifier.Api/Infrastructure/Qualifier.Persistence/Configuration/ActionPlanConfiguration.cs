using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ActionPlanConfiguration
    {
        public ActionPlanConfiguration(EntityTypeBuilder<ActionPlanEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ActionPlan");
            //entityBuilder.HasKey(x => x.actionPlanId);
            //entityBuilder.Property(x => x.actionPlanId).IsRequired();
            //entityBuilder.Property(x => x.breachId).IsRequired();
            //entityBuilder.Property(x => x.evaluationId).IsRequired();
            //entityBuilder.Property(x => x.standardId).IsRequired();
            //entityBuilder.Property(x => x.title).IsRequired();
            //entityBuilder.Property(x => x.responsibleId).IsRequired();
            //entityBuilder.Property(x => x.startDate).IsRequired();
            //entityBuilder.Property(x => x.dueDate).IsRequired();
            //entityBuilder.Property(x => x.actionPlanStatusId).IsRequired();
            //entityBuilder.Property(x => x.actionPlanPriorityId).IsRequired();
            entityBuilder.ToTable("MAE_ACTION_PLAN");

            entityBuilder.HasKey(e => e.actionPlanId)
                         .HasName("CST_MAE_ACTION_PLAN_PK");

            entityBuilder.Property(e => e.actionPlanId)
                         .HasColumnName("N_ACTION_PLAN_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.breachId)
                         .HasColumnName("N_BREACH_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.evaluationId)
                         .HasColumnName("N_EVALUATION_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.standardId)
                         .HasColumnName("N_STANDARD_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.title)
                         .HasColumnName("C_TITLE")
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION");

            entityBuilder.Property(e => e.responsibleId)
                         .HasColumnName("N_RESPONSIBLE_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.startDate)
                         .HasColumnName("D_START_DATE")
                         .IsRequired();

            entityBuilder.Property(e => e.dueDate)
                         .HasColumnName("D_DUE_DATE")
                         .IsRequired();

            entityBuilder.Property(e => e.actionPlanStatusId)
                         .HasColumnName("N_ACTION_PLAN_STATUS_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.actionPlanPriorityId)
                         .HasColumnName("N_ACTION_PLAN_PRIORITY_ID_FK")
                         .IsRequired();

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
        }
    }
}


