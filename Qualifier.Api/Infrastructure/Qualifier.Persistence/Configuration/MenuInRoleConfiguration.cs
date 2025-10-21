using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MenuInRoleConfiguration
    {
        public MenuInRoleConfiguration(EntityTypeBuilder<MenuInRoleEntity> entityBuilder)
        {
            entityBuilder.ToTable("MenuInRole");
            entityBuilder.HasKey(x => x.menuInRoleId);
            entityBuilder.Property(x => x.menuInRoleId).IsRequired();
            entityBuilder.Property(x => x.menuId).IsRequired();
            entityBuilder.Property(x => x.roleId).IsRequired();
            entityBuilder.Property(x => x.order).IsRequired();

            entityBuilder.HasOne(x => x.menu)
            .WithMany(x => x.menuInRoles)
            .HasForeignKey(x => x.menuId);

            entityBuilder.HasOne(x => x.role)
            .WithMany(x => x.menuInRoles)
            .HasForeignKey(x => x.roleId);
        }
    }
}


