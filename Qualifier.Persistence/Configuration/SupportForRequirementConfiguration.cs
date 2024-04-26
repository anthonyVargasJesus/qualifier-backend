using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class SupportForRequirementConfiguration
    {
        public SupportForRequirementConfiguration(EntityTypeBuilder<SupportForRequirementEntity> entityBuilder)
        {
            entityBuilder.ToTable("SupportForRequirement");
            entityBuilder.HasKey(x => x.supportForRequirementId);
            entityBuilder.Property(x => x.supportForRequirementId).IsRequired();
            entityBuilder.Property(x => x.documentationId).IsRequired();
            entityBuilder.Property(x => x.requirementId).IsRequired();

            //entityBuilder.HasOne(x => x.documentation)
            //.WithMany(x => x.supportForRequirements)
            //.HasForeignKey(x => x.documentationId);

            //entityBuilder.HasOne(x => x.requirement)
            //.WithMany(x => x.supportForRequirements)
            //.HasForeignKey(x => x.requirementId);
        }
    }
}


