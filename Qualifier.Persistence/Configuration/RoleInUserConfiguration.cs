using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
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

            entityBuilder.HasOne(x => x.role)
            .WithMany(x => x.roleInUsers)
            .HasForeignKey(x => x.roleId);

            entityBuilder.HasOne(x => x.user)
            .WithMany(x => x.roleInUsers)
            .HasForeignKey(x => x.userId);
        }
    }
}


