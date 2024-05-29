using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class CustodianConfiguration
    {
        public CustodianConfiguration(EntityTypeBuilder<CustodianEntity> entityBuilder)
        {
            entityBuilder.ToTable("Custodian");
            entityBuilder.HasKey(x => x.custodianId);
            entityBuilder.Property(x => x.custodianId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
        }
    }
}

