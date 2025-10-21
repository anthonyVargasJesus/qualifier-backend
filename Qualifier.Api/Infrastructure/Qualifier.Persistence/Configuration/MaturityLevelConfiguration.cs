using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;


namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MaturityLevelConfiguration
    {
        public MaturityLevelConfiguration(EntityTypeBuilder<MaturityLevelEntity> entityBuilder)
        {
            entityBuilder.ToTable("MaturityLevel");
            entityBuilder.HasKey(x => x.maturityLevelId);
            entityBuilder.Property(x => x.maturityLevelId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.description).IsRequired();
            entityBuilder.Property(x => x.abbreviation).IsRequired();
            entityBuilder.Property(x => x.value).IsRequired();
            entityBuilder.Property(x => x.color).IsRequired();
         
        }
    }
}
