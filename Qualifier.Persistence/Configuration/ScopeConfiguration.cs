using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ScopeConfiguration
    {
        public ScopeConfiguration(EntityTypeBuilder<ScopeEntity> entityBuilder)
        {
            entityBuilder.ToTable("Scope");
            entityBuilder.HasKey(x => x.scopeId);
            entityBuilder.Property(x => x.scopeId).IsRequired();
            entityBuilder.Property(x => x.isCurrent).IsRequired();
            entityBuilder.Property(x => x.date).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();

        }
    }
}

