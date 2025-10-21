using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ActionPlanPriorityConfiguration
    {
        public ActionPlanPriorityConfiguration(
            EntityTypeBuilder<ActionPlanPriorityEntity> entityBuilder)
        {
            entityBuilder.ToTable("ActionPlanPriority");
            entityBuilder.HasKey(x => x.actionPlanPriorityId);
            entityBuilder.Property(x => x.actionPlanPriorityId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();
        }
    }
}
