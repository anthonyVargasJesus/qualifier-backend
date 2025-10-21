using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class EvaluationStateConfiguration
    {
        public EvaluationStateConfiguration(EntityTypeBuilder<EvaluationStateEntity> entityBuilder)
        {
            entityBuilder.ToTable("EvaluationState");
            entityBuilder.HasKey(x => x.evaluationStateId);
            entityBuilder.Property(x => x.evaluationStateId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
        }
    }
}


