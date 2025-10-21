using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
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

        }
    }
}


