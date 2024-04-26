using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ConfidentialityLevelConfiguration
    {
        public ConfidentialityLevelConfiguration(EntityTypeBuilder<ConfidentialityLevelEntity> entityBuilder)
        {
            entityBuilder.ToTable("ConfidentialityLevel");
            entityBuilder.HasKey(x => x.confidentialityLevelId);
            entityBuilder.Property(x => x.confidentialityLevelId).IsRequired();
        }
    }
}


