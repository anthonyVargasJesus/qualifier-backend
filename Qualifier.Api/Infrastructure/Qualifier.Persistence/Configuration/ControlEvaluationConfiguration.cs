using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ControlEvaluationConfiguration
    {
        public ControlEvaluationConfiguration(EntityTypeBuilder<ControlEvaluationEntity> entityBuilder)
        {
            entityBuilder.ToTable("ControlEvaluation");
            entityBuilder.HasKey(x => x.controlEvaluationId);
            entityBuilder.Property(x => x.controlEvaluationId).IsRequired();
            entityBuilder.Property(x => x.evaluationId).IsRequired();
            entityBuilder.Property(x => x.controlId).IsRequired();
            entityBuilder.Property(x => x.maturityLevelId).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.Property(x => x.responsibleId).IsRequired();
            entityBuilder.Property(x => x.justification).IsRequired();
            entityBuilder.Property(x => x.improvementActions).IsRequired();
            entityBuilder.Property(x => x.controlDescription).IsRequired();
            entityBuilder.Property(x => x.controlType).IsRequired();
            entityBuilder.Property(x => x.companyId).IsRequired();

            entityBuilder.HasOne(x => x.evaluation)
            .WithMany(x => x.controlEvaluations)
            .HasForeignKey(x => x.evaluationId);

            entityBuilder.HasOne(x => x.control)
            .WithMany(x => x.controlEvaluations)
            .HasForeignKey(x => x.controlId);

            entityBuilder.HasOne(x => x.maturityLevel)
            .WithMany(x => x.controlEvaluations)
            .HasForeignKey(x => x.maturityLevelId);

            entityBuilder.HasOne(x => x.responsible)
            .WithMany(x => x.controlEvaluations)
            .HasForeignKey(x => x.responsibleId);
        }
    }
}


