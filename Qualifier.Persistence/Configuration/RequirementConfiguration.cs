using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class RequirementConfiguration
    {
        public RequirementConfiguration(EntityTypeBuilder<RequirementEntity> entityBuilder)
        {
            entityBuilder.ToTable("Requirement");
            entityBuilder.HasKey(x => x.requirementId);
            entityBuilder.Property(x => x.numeration).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.standardId).IsRequired();
            entityBuilder.Property(x => x.level).IsRequired();
            entityBuilder.Property(x => x.companyId).IsRequired();

            //entityBuilder.HasOne(x => x.Standard)
            //.WithMany(x => x.Requirement)
            //.HasForeignKey(x => x.standardId);

            entityBuilder.HasOne(x => x.requirement)
            .WithMany(x => x.requirements)
            .HasForeignKey(x => x.parentId);
        }
    }
}


