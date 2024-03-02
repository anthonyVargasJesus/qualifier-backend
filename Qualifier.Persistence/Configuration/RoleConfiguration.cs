using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class RoleConfiguration
    {
        public RoleConfiguration(EntityTypeBuilder<RoleEntity> entityBuilder)
        {
            entityBuilder.ToTable("Role");
            entityBuilder.HasKey(x => x.roleId);
            entityBuilder.Property(x => x.roleId).IsRequired();
        }
    }
}


