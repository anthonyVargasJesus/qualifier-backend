using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class SubprocessConfiguration
    {
        public SubprocessConfiguration(EntityTypeBuilder<SubprocessEntity> entityBuilder)
        {
            entityBuilder.ToTable("Subprocess");
            entityBuilder.HasKey(x => x.subprocessId);
            entityBuilder.Property(x => x.subprocessId).IsRequired();
            entityBuilder.Property(x => x.code).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.macroprocessId).IsRequired();

            //entityBuilder.HasOne(x => x.macroprocess)
            //.WithMany(x => x.subprocesss)
            //.HasForeignKey(x => x.macroprocessId);
        }
    }
}


