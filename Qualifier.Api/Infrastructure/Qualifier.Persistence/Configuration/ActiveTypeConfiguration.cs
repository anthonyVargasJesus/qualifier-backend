using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ActiveTypeConfiguration
    {
        public ActiveTypeConfiguration(EntityTypeBuilder<ActiveTypeEntity> entityBuilder)
        {
            entityBuilder.ToTable("ActiveType");
            entityBuilder.HasKey(x => x.activeTypeId);
            entityBuilder.Property(x => x.activeTypeId).IsRequired();
        }
    }
}


