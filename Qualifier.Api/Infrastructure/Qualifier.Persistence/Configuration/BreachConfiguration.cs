using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class BreachConfiguration
    {
        public BreachConfiguration(EntityTypeBuilder<BreachEntity> entityBuilder)
        {
            entityBuilder.ToTable("Breach");
            entityBuilder.HasKey(x => x.breachId);
            entityBuilder.Property(x => x.breachId).IsRequired();
            entityBuilder.Property(x => x.evaluationId).IsRequired();
            entityBuilder.Property(x => x.standardId).IsRequired();
            entityBuilder.Property(x => x.requirementId).IsRequired();
            entityBuilder.Property(x => x.controlId).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();
            entityBuilder.Property(x => x.breachSeverityId).IsRequired();
            entityBuilder.Property(x => x.breachStatusId).IsRequired();
            entityBuilder.Property(x => x.responsibleId).IsRequired();

        }
    }
}
