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

            //entityBuilder.HasOne(x => x.documentation)
            //.WithMany(x => x.supportForControls)
            //.HasForeignKey(x => x.documentationId);

            //entityBuilder.HasOne(x => x.control)
            //.WithMany(x => x.supportForControls)
            //.HasForeignKey(x => x.controlId);
        }
    }
}

