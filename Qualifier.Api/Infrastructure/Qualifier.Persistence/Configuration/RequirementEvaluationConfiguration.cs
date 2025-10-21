using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RequirementEvaluationConfiguration
    {
        public RequirementEvaluationConfiguration(EntityTypeBuilder<RequirementEvaluationEntity> entityBuilder)
        {
            entityBuilder.ToTable("RequirementEvaluation");
            entityBuilder.HasKey(x => x.requirementEvaluationId);
            entityBuilder.Property(x => x.requirementEvaluationId).IsRequired();
            entityBuilder.Property(x => x.evaluationId).IsRequired();
            entityBuilder.Property(x => x.requirementId).IsRequired();
            entityBuilder.Property(x => x.maturityLevelId).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.Property(x => x.responsibleId).IsRequired();
            entityBuilder.Property(x => x.justification).IsRequired();
            entityBuilder.Property(x => x.improvementActions).IsRequired();
            entityBuilder.Property(x => x.companyId).IsRequired();

            entityBuilder.HasOne(x => x.evaluation)
            .WithMany(x => x.requirementEvaluations)
            .HasForeignKey(x => x.evaluationId);

            entityBuilder.HasOne(x => x.requirement)
            .WithMany(x => x.requirementEvaluations)
            .HasForeignKey(x => x.requirementId);

            entityBuilder.HasOne(x => x.maturityLevel)
            .WithMany(x => x.requirementEvaluations)
            .HasForeignKey(x => x.maturityLevelId);

            entityBuilder.HasOne(x => x.responsible)
            .WithMany(x => x.requirementEvaluations)
            .HasForeignKey(x => x.responsibleId);
        }
    }
}
