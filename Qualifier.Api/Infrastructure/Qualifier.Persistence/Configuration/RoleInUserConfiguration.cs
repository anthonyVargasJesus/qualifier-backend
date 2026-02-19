using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RoleInUserConfiguration
    {
        public RoleInUserConfiguration(EntityTypeBuilder<RoleInUserEntity> entityBuilder)
        {
            //entityBuilder.ToTable("RoleInUser");
            //entityBuilder.HasKey(x => x.roleInUserId);
            //entityBuilder.Property(x => x.roleInUserId).IsRequired();
            //entityBuilder.Property(x => x.roleId).IsRequired();
            //entityBuilder.Property(x => x.userId).IsRequired();

            entityBuilder.ToTable("MAE_ROLE_IN_USER");

            entityBuilder.HasKey(e => e.roleInUserId)
                         .HasName("CST_MAE_ROLE_IN_USER_PK");

            entityBuilder.Property(e => e.roleInUserId)
                         .HasColumnName("N_ROLE_IN_USER_ID_PK")
                         .IsRequired();

            entityBuilder.Property(e => e.roleId)
                         .HasColumnName("N_ROLE_ID")
                         .IsRequired();

            entityBuilder.Property(e => e.userId)
                         .HasColumnName("N_USER_ID")
                         .IsRequired();

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

            entityBuilder.Property(e => e.roleInUserId)
.HasColumnName("N_ROLE_IN_USER_ID_PK")
.ValueGeneratedOnAdd()
.IsRequired();

        }
    }
}


