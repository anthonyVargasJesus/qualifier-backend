using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ControlGroupConfiguration
    {
        public ControlGroupConfiguration(EntityTypeBuilder<ControlGroupEntity> entityBuilder)
        {
            entityBuilder.ToTable("ControlGroup");
            entityBuilder.HasKey(x => x.controlGroupId);
            entityBuilder.Property(x => x.controlGroupId).IsRequired();
            entityBuilder.Property(x => x.number).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();

        }
    }
}


