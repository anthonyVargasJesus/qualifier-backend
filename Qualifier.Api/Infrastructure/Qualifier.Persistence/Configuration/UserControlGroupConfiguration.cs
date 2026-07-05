using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class UserControlGroupConfiguration
    {
        public UserControlGroupConfiguration(EntityTypeBuilder<UserControlGroupEntity> entityBuilder)
        {
            entityBuilder.ToTable("MAE_USER_CONTROL_GROUP");

            entityBuilder.HasKey(e => e.userControlGroupId)
                         .HasName("CST_MAE_USER_CONTROL_GROUP_PK");

            entityBuilder.Property(e => e.userControlGroupId)
                         .HasColumnName("N_USER_CONTROL_GROUP_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.userId)
                         .HasColumnName("N_USER_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.controlGroupId)
                         .HasColumnName("N_CONTROL_GROUP_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.standardId)
                         .HasColumnName("N_STANDARD_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.companyId)
                         .HasColumnName("N_COMPANY_ID_FK")
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

            entityBuilder.HasOne(e => e.user).WithMany().HasForeignKey(e => e.userId);
            entityBuilder.HasOne(e => e.controlGroup).WithMany().HasForeignKey(e => e.controlGroupId);
        }
    }
}
