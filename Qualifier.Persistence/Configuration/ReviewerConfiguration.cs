using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ReviewerConfiguration
    {
        public ReviewerConfiguration(EntityTypeBuilder<ReviewerEntity> entityBuilder)
        {
            entityBuilder.ToTable("Reviewer");
            entityBuilder.HasKey(x => x.reviewerId);
            entityBuilder.Property(x => x.reviewerId).IsRequired();
            entityBuilder.Property(x => x.versionId).IsRequired();

            //entityBuilder.HasOne(x => x.personal)
            //.WithMany(x => x.reviewers)
            //.HasForeignKey(x => x.personalId);

            //entityBuilder.HasOne(x => x.responsible)
            //.WithMany(x => x.reviewers)
            //.HasForeignKey(x => x.responsibleId);

            //entityBuilder.HasOne(x => x.version)
            //.WithMany(x => x.reviewers)
            //.HasForeignKey(x => x.versionId);

            //entityBuilder.HasOne(x => x.company)
            //.WithMany(x => x.reviewers)
            //.HasForeignKey(x => x.companyId);
        }
    }
}


