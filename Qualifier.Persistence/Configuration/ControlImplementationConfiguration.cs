using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ControlImplementationConfiguration
    {
        public ControlImplementationConfiguration(EntityTypeBuilder<ControlImplementationEntity> entityBuilder)
        {
            entityBuilder.ToTable("ControlImplementation");
            entityBuilder.HasKey(x => x.controlImplementationId);
            entityBuilder.Property(x => x.controlImplementationId).IsRequired();
            entityBuilder.Property(x => x.activities).IsRequired();
            entityBuilder.Property(x => x.startDate).IsRequired();
            entityBuilder.Property(x => x.responsibleId).IsRequired();

        }
    }
}


