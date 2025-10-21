using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ResponsibleConfiguration
    {
        public ResponsibleConfiguration(EntityTypeBuilder<ResponsibleEntity> entityBuilder)
        {
            entityBuilder.ToTable("Responsible");
            entityBuilder.HasKey(x => x.responsibleId);
            entityBuilder.Property(x => x.responsibleId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.companyId).IsRequired();

        }
    }
}


