using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class OptionInMenuConfiguration
    {
        public OptionInMenuConfiguration(EntityTypeBuilder<OptionInMenuEntity> entityBuilder)
        {
            entityBuilder.ToTable("OptionInMenu");
            entityBuilder.HasKey(x => x.optionInMenuId);
            entityBuilder.Property(x => x.optionInMenuId).IsRequired();
            entityBuilder.Property(x => x.menuId).IsRequired();
            entityBuilder.Property(x => x.optionId).IsRequired();
            entityBuilder.Property(x => x.order).IsRequired();
        }
    }
}

