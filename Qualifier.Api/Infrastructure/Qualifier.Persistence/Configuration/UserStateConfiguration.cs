using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class UserStateConfiguration
    {
        public UserStateConfiguration(EntityTypeBuilder<UserStateEntity> entityBuilder)
        {
            //entityBuilder.ToTable("UserState");
            //entityBuilder.HasKey(x => x.userStateId);
            //entityBuilder.Property(x => x.userStateId).IsRequired();
            //entityBuilder.Property(x => x.value).IsRequired();

            entityBuilder.ToTable("MAE_USER_STATE");

            entityBuilder.HasKey(e => e.userStateId)
                         .HasName("CST_MAE_USER_STATE_PK");

            entityBuilder.Property(e => e.userStateId)
                         .HasColumnName("N_USER_STATE_ID_PK")
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME");

            entityBuilder.Property(e => e.value)
                         .HasColumnName("N_VALUE")
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

            entityBuilder.Property(e => e.userStateId)
 .HasColumnName("N_USER_STATE_ID_PK")
 .ValueGeneratedOnAdd()
 .IsRequired();

        }
    }
}


