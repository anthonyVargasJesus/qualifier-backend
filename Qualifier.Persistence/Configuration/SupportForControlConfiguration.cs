using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class SupportForControlConfiguration
    {
        public SupportForControlConfiguration(EntityTypeBuilder<SupportForControlEntity> entityBuilder)
        {
            entityBuilder.ToTable("SupportForControl");
            entityBuilder.HasKey(x => x.supportForControlId);
            entityBuilder.Property(x => x.supportForControlId).IsRequired();
            entityBuilder.Property(x => x.documentationId).IsRequired();
            entityBuilder.Property(x => x.controlId).IsRequired();

        }
    }
}

