using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MenuConfiguration
    {
        public MenuConfiguration(EntityTypeBuilder<MenuEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Menu");
            //entityBuilder.HasKey(x => x.menuId);
            //entityBuilder.Property(x => x.menuId).IsRequired();

            entityBuilder.ToTable("MAE_MENU");

            entityBuilder.HasKey(e => e.menuId)
                         .HasName("CST_MAE_MENU_PK");

            entityBuilder.Property(e => e.menuId)
                         .HasColumnName("N_MENU_ID_PK")
                         .IsRequired();


            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .IsRequired();

            entityBuilder.Property(e => e.image)
                         .HasColumnName("C_IMAGE");

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

            entityBuilder.Property(e => e.menuId)
.HasColumnName("N_MENU_ID_PK")
.ValueGeneratedOnAdd()
.IsRequired();

        }
    }
}


