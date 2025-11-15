using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class OptionInMenuConfiguration
    {
        public OptionInMenuConfiguration(EntityTypeBuilder<OptionInMenuEntity> entityBuilder)
        {
            //entityBuilder.ToTable("OptionInMenu");
            //entityBuilder.HasKey(x => x.optionInMenuId);
            //entityBuilder.Property(x => x.optionInMenuId).IsRequired();
            //entityBuilder.Property(x => x.menuId).IsRequired();
            //entityBuilder.Property(x => x.optionId).IsRequired();
            //entityBuilder.Property(x => x.order).IsRequired();

            entityBuilder.ToTable("MAE_OPTION_IN_MENU");

            entityBuilder.HasKey(e => e.optionInMenuId)
                         .HasName("CST_MAE_OPTION_IN_MENU_PK");

            entityBuilder.Property(e => e.optionInMenuId)
                         .HasColumnName("N_OPTION_IN_MENU_ID_PK")
                         .IsRequired();

            entityBuilder.Property(e => e.menuId)
                         .HasColumnName("N_MENU_ID")
                         .IsRequired();

            entityBuilder.Property(e => e.optionId)
                         .HasColumnName("N_OPTION_ID")
                         .IsRequired();

            entityBuilder.Property(e => e.order)
                         .HasColumnName("N_ORDER")
                         .IsRequired();

            entityBuilder.Property(e => e.companyId)
                         .HasColumnName("N_COMPANY_ID");

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

            entityBuilder.Property(e => e.optionInMenuId)
.HasColumnName("N_OPTION_IN_MENU_ID_PK")
.ValueGeneratedOnAdd()
.IsRequired();

        }
    }
}

