using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class CustodianConfiguration
    {
        public CustodianConfiguration(EntityTypeBuilder<CustodianEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Custodian");
            //entityBuilder.HasKey(x => x.custodianId);
            //entityBuilder.Property(x => x.custodianId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.ToTable("MAE_CUSTODIAN");

            entityBuilder.HasKey(e => e.custodianId)
                .HasName("CST_MAE_CUSTODIAN_PK");

            entityBuilder.Property(e => e.custodianId)
                .HasColumnName("N_CUSTODIAN_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .IsRequired();

            entityBuilder.Property(e => e.companyId)
                .HasColumnName("N_COMPANY_ID_FK");

            entityBuilder.Property(e => e.creationDate)
                .HasColumnName("D_CREATION_DATE");

            entityBuilder.Property(e => e.updateDate)
                .HasColumnName("D_UPDATE_DATE");

            entityBuilder.Property(e => e.creationUserId)
                .HasColumnName("N_CREATION_USER_ID");

            entityBuilder.Property(e => e.updateUserId)
                .HasColumnName("N_UPDATE_USER_ID");

            entityBuilder.Property(e => e.isDeleted)
                .HasColumnName("N_IS_DELETED");

            entityBuilder.Property(e => e.code)
                .HasColumnName("C_CODE");
        }
    }
}

