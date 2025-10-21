using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class LocationConfiguration
    {
        public LocationConfiguration(EntityTypeBuilder<LocationEntity> entityBuilder)
        {
            entityBuilder.ToTable("Location");
            entityBuilder.HasKey(x => x.locationId);
            entityBuilder.Property(x => x.locationId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();

        }
    }
}


