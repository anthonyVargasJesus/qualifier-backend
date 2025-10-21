using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MenuConfiguration
    {
        public MenuConfiguration(EntityTypeBuilder<MenuEntity> entityBuilder)
        {
            entityBuilder.ToTable("Menu");
            entityBuilder.HasKey(x => x.menuId);
            entityBuilder.Property(x => x.menuId).IsRequired();

        }
    }
}


