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
        }
    }
}


