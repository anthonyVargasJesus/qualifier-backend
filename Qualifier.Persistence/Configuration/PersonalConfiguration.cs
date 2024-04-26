using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class PersonalConfiguration
    {
        public PersonalConfiguration(EntityTypeBuilder<PersonalEntity> entityBuilder)
        {
            entityBuilder.ToTable("Personal");
            entityBuilder.HasKey(x => x.personalId);
            entityBuilder.Property(x => x.personalId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.firstName).IsRequired();
        }
    }
}


