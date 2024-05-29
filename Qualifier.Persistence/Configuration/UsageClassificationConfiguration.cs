using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class UsageClassificationConfiguration
    {
        public UsageClassificationConfiguration(EntityTypeBuilder<UsageClassificationEntity> entityBuilder)
        {
            entityBuilder.ToTable("UsageClassification");
            entityBuilder.HasKey(x => x.usageClassificationId);
            entityBuilder.Property(x => x.usageClassificationId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();

        }
    }
}


