using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ActionPlanStatusConfiguration
    {
        public ActionPlanStatusConfiguration(
            EntityTypeBuilder<ActionPlanStatusEntity> entityBuilder)
        {
            entityBuilder.ToTable("ActionPlanStatus");
            entityBuilder.HasKey(x => x.actionPlanStatusId);
            entityBuilder.Property(x => x.actionPlanStatusId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();
        }
    }
}


