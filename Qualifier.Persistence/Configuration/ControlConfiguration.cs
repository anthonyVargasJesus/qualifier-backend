using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ControlConfiguration
    {
        public ControlConfiguration(EntityTypeBuilder<ControlEntity> entityBuilder)
        {
            entityBuilder.ToTable("Control");
            entityBuilder.HasKey(x => x.controlId);
            entityBuilder.Property(x => x.controlId).IsRequired();
            entityBuilder.Property(x => x.number).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.controlGroupId).IsRequired();
            entityBuilder.Property(x => x.standardId).IsRequired();
            entityBuilder.Property(x => x.companyId).IsRequired();

            //entityBuilder.HasOne(x => x.ControlGroup)
            //.WithMany(x => x.Control)
            //.HasForeignKey(x => x.controlGroupId);

            //entityBuilder.HasOne(x => x.Standard)
            //.WithMany(x => x.Control)
            //.HasForeignKey(x => x.standardId);
        }
    }
}


