using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RoleConfiguration
    {
        public RoleConfiguration(EntityTypeBuilder<RoleEntity> entityBuilder)
        {
            entityBuilder.ToTable("Role");
            entityBuilder.HasKey(x => x.roleId);
            entityBuilder.Property(x => x.roleId).IsRequired();

            //            entityBuilder.ToTable("MAE_ROLE");

            //            entityBuilder.HasKey(e => e.roleId)
            //                         .HasName("CST_MAE_ROLE_PK");

            //            entityBuilder.Property(e => e.roleId)
            //                         .HasColumnName("N_ROLE_ID_PK")
            //                         .IsRequired();

            //            entityBuilder.Property(e => e.code)
            //                         .HasColumnName("C_CODE");

            //            entityBuilder.Property(e => e.name)
            //                         .HasColumnName("C_NAME");

            //            entityBuilder.Property(e => e.creationDate)
            //                         .HasColumnName("D_CREATION_DATE");

            //            entityBuilder.Property(e => e.updateDate)
            //                         .HasColumnName("D_UPDATE_DATE");

            //            entityBuilder.Property(e => e.creationUserId)
            //                         .HasColumnName("N_CREATION_USER_ID");

            //            entityBuilder.Property(e => e.updateUserId)
            //                         .HasColumnName("N_UPDATE_USER_ID");

            //            entityBuilder.Property(e => e.isDeleted)
            //                         .HasColumnName("N_IS_DELETED");

            //            entityBuilder.Property(e => e.companyId)
            //                         .HasColumnName("N_COMPANY_ID");

            //            entityBuilder.Property(e => e.roleId)
            //.HasColumnName("N_ROLE_ID_PK")
            //.ValueGeneratedOnAdd()
            //.IsRequired();

        }
    }
}


