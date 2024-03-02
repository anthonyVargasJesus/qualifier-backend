using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class OptionConfiguration
    {
        public OptionConfiguration(EntityTypeBuilder<OptionEntity> entityBuilder)
        {
            entityBuilder.ToTable("Option");
            entityBuilder.HasKey(x => x.optionId);
            entityBuilder.Property(x => x.optionId).IsRequired();
            entityBuilder.Property(x => x.isMobile).IsRequired();
        }
    }
}


