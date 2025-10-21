using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MacroprocessConfiguration
    {
        public MacroprocessConfiguration(EntityTypeBuilder<MacroprocessEntity> entityBuilder)
        {
            entityBuilder.ToTable("Macroprocess");
            entityBuilder.HasKey(x => x.macroprocessId);
            entityBuilder.Property(x => x.macroprocessId).IsRequired();
            entityBuilder.Property(x => x.code).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();

        }
    }
}


