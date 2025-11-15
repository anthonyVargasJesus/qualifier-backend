using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class OptionConfiguration
    {
        public OptionConfiguration(EntityTypeBuilder<OptionEntity> entityBuilder)
        {
            entityBuilder.ToTable("MAE_OPTION");

            entityBuilder.HasKey(e => e.optionId)
                         .HasName("CST_MAE_OPTION_PK");

            entityBuilder.Property(e => e.optionId)
                         .HasColumnName("N_OPTION_ID_PK")
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .IsRequired();

            entityBuilder.Property(e => e.image)
                         .HasColumnName("C_IMAGE");

            entityBuilder.Property(e => e.url)
                         .HasColumnName("C_URL");

            entityBuilder.Property(e => e.isMobile)
                         .HasColumnName("N_IS_MOBILE");

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
                         .HasColumnName("N_COMPANY_ID");

            entityBuilder.Property(e => e.optionId)
                .HasColumnName("N_OPTION_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

        }
    }
}

