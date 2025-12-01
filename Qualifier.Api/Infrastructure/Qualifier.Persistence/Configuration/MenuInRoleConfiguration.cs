using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MenuInRoleConfiguration
    {
        public MenuInRoleConfiguration(EntityTypeBuilder<MenuInRoleEntity> entityBuilder)
        {
            entityBuilder.ToTable("MenuInRole");
            entityBuilder.HasKey(x => x.menuInRoleId);
            entityBuilder.Property(x => x.menuInRoleId).IsRequired();
            entityBuilder.Property(x => x.menuId).IsRequired();
            entityBuilder.Property(x => x.roleId).IsRequired();
            entityBuilder.Property(x => x.order).IsRequired();

            //entityBuilder.HasOne(x => x.menu)
            //.WithMany(x => x.menuInRoles)
            //.HasForeignKey(x => x.menuId);

            //entityBuilder.HasOne(x => x.role)
            //.WithMany(x => x.menuInRoles)
            //.HasForeignKey(x => x.roleId);

            //entityBuilder.ToTable("MAE_MENU_IN_ROLE");

            //entityBuilder.HasKey(e => e.menuInRoleId)
            //             .HasName("CST_MAE_MENU_IN_ROLE_PK");

            //entityBuilder.Property(e => e.menuInRoleId)
            //             .HasColumnName("N_MENU_IN_ROLE_ID_PK")
            //             .IsRequired();

            //entityBuilder.Property(e => e.menuId)
            //             .HasColumnName("N_MENU_ID")
            //             .IsRequired();

            //entityBuilder.Property(e => e.roleId)
            //             .HasColumnName("N_ROLE_ID")
            //             .IsRequired();

            //entityBuilder.Property(e => e.order)
            //             .HasColumnName("N_ORDER")
            //             .IsRequired();

            //entityBuilder.Property(e => e.creationDate)
            //             .HasColumnName("D_CREATION_DATE");

            //entityBuilder.Property(e => e.updateDate)
            //             .HasColumnName("D_UPDATE_DATE");

            //entityBuilder.Property(e => e.creationUserId)
            //             .HasColumnName("N_CREATION_USER_ID");

            //entityBuilder.Property(e => e.updateUserId)
            //             .HasColumnName("N_UPDATE_USER_ID");

            //entityBuilder.Property(e => e.isDeleted)
            //             .HasColumnName("N_IS_DELETED");

            //entityBuilder.Property(e => e.companyId)
            //             .HasColumnName("N_COMPANY_ID");
        }
    }
}


