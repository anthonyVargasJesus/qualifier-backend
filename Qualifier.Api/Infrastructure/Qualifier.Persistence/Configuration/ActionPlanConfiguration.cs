using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ActionPlanConfiguration
    {
        public ActionPlanConfiguration(EntityTypeBuilder<ActionPlanEntity> entityBuilder)
        {
            entityBuilder.ToTable("ActionPlan");
            entityBuilder.HasKey(x => x.actionPlanId);
            entityBuilder.Property(x => x.actionPlanId).IsRequired();
            entityBuilder.Property(x => x.breachId).IsRequired();
            entityBuilder.Property(x => x.evaluationId).IsRequired();
            entityBuilder.Property(x => x.standardId).IsRequired();
            entityBuilder.Property(x => x.title).IsRequired();
            entityBuilder.Property(x => x.responsibleId).IsRequired();
            entityBuilder.Property(x => x.startDate).IsRequired();
            entityBuilder.Property(x => x.dueDate).IsRequired();
            entityBuilder.Property(x => x.actionPlanStatusId).IsRequired();
            entityBuilder.Property(x => x.actionPlanPriorityId).IsRequired();
        }
    }
}


