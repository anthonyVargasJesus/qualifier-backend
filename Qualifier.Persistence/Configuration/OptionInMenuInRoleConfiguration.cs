using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class OptionInMenuInRoleConfiguration
    {
        public OptionInMenuInRoleConfiguration(EntityTypeBuilder<OptionInMenuInRoleEntity> entityBuilder)
        {
            entityBuilder.ToTable("OptionInMenuInRole");
            entityBuilder.HasKey(x => x.optionInMenuInRoleId);
            entityBuilder.Property(x => x.optionInMenuInRoleId).IsRequired();
            entityBuilder.Property(x => x.optionId).IsRequired();
            entityBuilder.Property(x => x.order).IsRequired();
            entityBuilder.Property(x => x.menuId).IsRequired();
            entityBuilder.Property(x => x.roleId).IsRequired();

        }
    }
}


