using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class CompanyConfiguration
    {
        public CompanyConfiguration(EntityTypeBuilder<CompanyEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Company");
            //entityBuilder.HasKey(x => x.companyId);
            //entityBuilder.Property(x => x.companyId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();

            entityBuilder.ToTable("MAE_COMPANY");

            entityBuilder.HasKey(e => e.companyId)
                         .HasName("CST_MAE_COMPANY_PK");

            entityBuilder.Property(e => e.companyId)
                         .HasColumnName("N_COMPANY_ID_PK")
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .HasMaxLength(200)
                         .IsRequired();

            entityBuilder.Property(e => e.abbreviation)
                         .HasColumnName("C_ABBREVIATION")
                         .HasMaxLength(10);

            entityBuilder.Property(e => e.slogan)
                         .HasColumnName("C_SLOGAN")
                         .HasMaxLength(200);

            entityBuilder.Property(e => e.logo)
                         .HasColumnName("C_LOGO")
                         .HasMaxLength(500);

            entityBuilder.Property(e => e.address)
                         .HasColumnName("C_ADDRESS");

            entityBuilder.Property(e => e.phone)
                         .HasColumnName("C_PHONE");

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

            entityBuilder.Property(e => e.companyId)
             .HasColumnName("N_COMPANY_ID_PK")
             .ValueGeneratedOnAdd()
             .IsRequired();

        }
    }
}


