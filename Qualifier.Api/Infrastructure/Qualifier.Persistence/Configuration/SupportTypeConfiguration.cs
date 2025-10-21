using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class SupportTypeConfiguration
    {
        public SupportTypeConfiguration(EntityTypeBuilder<SupportTypeEntity> entityBuilder)
        {
            entityBuilder.ToTable("SupportType");
            entityBuilder.HasKey(x => x.supportTypeId);
            entityBuilder.Property(x => x.supportTypeId).IsRequired();

        }
    }
}


