using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class EvaluationConfiguration
    {
        public EvaluationConfiguration(EntityTypeBuilder<EvaluationEntity> entityBuilder)
        {
            entityBuilder.ToTable("Evaluation");
            entityBuilder.HasKey(x => x.evaluationId);
            entityBuilder.Property(x => x.evaluationId).IsRequired();
            entityBuilder.Property(x => x.startDate).IsRequired();
            entityBuilder.Property(x => x.evaluationStateId).IsRequired();
            entityBuilder.HasOne(x => x.standard)
            .WithMany(x => x.evaluations)
            .HasForeignKey(x => x.standardId);
        }
    }
}


