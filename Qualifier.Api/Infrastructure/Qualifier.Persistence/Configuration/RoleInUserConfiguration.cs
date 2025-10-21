using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RoleInUserConfiguration
    {
        public RoleInUserConfiguration(EntityTypeBuilder<RoleInUserEntity> entityBuilder)
        {
            entityBuilder.ToTable("RoleInUser");
            entityBuilder.HasKey(x => x.roleInUserId);
            entityBuilder.Property(x => x.roleInUserId).IsRequired();
            entityBuilder.Property(x => x.roleId).IsRequired();
            entityBuilder.Property(x => x.userId).IsRequired();
        }
    }
}


