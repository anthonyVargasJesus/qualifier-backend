using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class UserStateConfiguration
    {
        public UserStateConfiguration(EntityTypeBuilder<UserStateEntity> entityBuilder)
        {
            entityBuilder.ToTable("UserState");
            entityBuilder.HasKey(x => x.userStateId);
            entityBuilder.Property(x => x.userStateId).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
        }
    }
}


