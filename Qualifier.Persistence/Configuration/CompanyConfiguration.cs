using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class CompanyConfiguration
    {
        public CompanyConfiguration(EntityTypeBuilder<CompanyEntity> entityBuilder)
        {
            entityBuilder.ToTable("Company");
            entityBuilder.HasKey(x => x.companyId);
            entityBuilder.Property(x => x.companyId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
        }
    }
}


