using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class PolicyConfiguration
    {
        public PolicyConfiguration(EntityTypeBuilder<PolicyEntity> entityBuilder)
        {
            entityBuilder.ToTable("Policy");
            entityBuilder.HasKey(x => x.policyId);
            entityBuilder.Property(x => x.policyId).IsRequired();
            entityBuilder.Property(x => x.isCurrent).IsRequired();
            entityBuilder.Property(x => x.date).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();

        }
    }
}
