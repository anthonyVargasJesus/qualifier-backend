using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ControlTypeConfiguration
    {
        public ControlTypeConfiguration(EntityTypeBuilder<ControlTypeEntity> entityBuilder)
        {
            entityBuilder.ToTable("ControlType");
            entityBuilder.HasKey(x => x.controlTypeId);
            entityBuilder.Property(x => x.controlTypeId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.minimum).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();

        }
    }
}

