using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<UserEntity> entityBuilder)
        {
            entityBuilder.ToTable("User");
            entityBuilder.HasKey(x => x.userId);
            entityBuilder.Property(x => x.userId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.email).IsRequired();
            entityBuilder.Property(x => x.userStateId).IsRequired();

            entityBuilder.HasOne(x => x.userState)
            .WithMany(x => x.users)
            .HasForeignKey(x => x.userStateId);
        }
    }
}


