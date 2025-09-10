using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class RequirementInDefaultRiskConfiguration
    {
        public RequirementInDefaultRiskConfiguration(EntityTypeBuilder<RequirementInDefaultRiskEntity> entityBuilder)
        {
            entityBuilder.ToTable("RequirementInDefaultRisk");
            entityBuilder.HasKey(x => x.requirementInDefaultRiskId);
            entityBuilder.Property(x => x.requirementInDefaultRiskId).IsRequired();
            entityBuilder.Property(x => x.defaultRiskId).IsRequired();
            entityBuilder.Property(x => x.requirementId).IsRequired();
        }
    }
}


