using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<UserEntity> entityBuilder)
        {
            entityBuilder.ToTable("User");
            entityBuilder.HasKey(x => x.userId);
            entityBuilder.Property(x => x.userId).IsRequired();
            entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.Property(x => x.email).IsRequired();
            entityBuilder.Property(x => x.userStateId).IsRequired();
            entityBuilder.Property(x => x.documentNumber).IsRequired();

            entityBuilder.HasOne(x => x.userState)
            .WithMany(x => x.users)
            .HasForeignKey(x => x.userStateId);

            entityBuilder.HasOne(x => x.standard)
           .WithMany(x => x.users)
           .HasForeignKey(x => x.standardId);

            //setName(entityBuilder);

        }

        private void setName(EntityTypeBuilder<UserEntity> entity)
        {
            entity.ToTable("MAE_USER");

            entity.HasKey(e => e.userId)
                  .HasName("CST_MAE_USER_PK");

            entity.Property(e => e.userId)
                  .HasColumnName("N_USER_ID_PK")
                  .IsRequired(); // PK

            entity.Property(e => e.name)
                  .HasColumnName("C_NAME")
                  .IsRequired();

            entity.Property(e => e.middleName)
                  .HasColumnName("C_MIDDLE_NAME");

            entity.Property(e => e.firstName)
                  .HasColumnName("C_FIRST_NAME")
                  .IsRequired();

            entity.Property(e => e.lastName)
                  .HasColumnName("C_LAST_NAME");

            entity.Property(e => e.email)
                  .HasColumnName("C_EMAIL")
                  .IsRequired();

            entity.Property(e => e.phone)
                  .HasColumnName("C_PHONE");

            entity.Property(e => e.uid)
                  .HasColumnName("C_UID");

            entity.Property(e => e.userStateId)
                  .HasColumnName("N_USER_STATE_ID")
                  .IsRequired();

            entity.Property(e => e.image)
                  .HasColumnName("C_IMAGE");

            entity.Property(e => e.userName)
                  .HasColumnName("C_USER_NAME");

            entity.Property(e => e.creationDate)
                  .HasColumnName("D_CREATION_DATE");

            entity.Property(e => e.updateDate)
                  .HasColumnName("D_UPDATE_DATE");

            entity.Property(e => e.creationUserId)
                  .HasColumnName("N_CREATION_USER_ID");

            entity.Property(e => e.updateUserId)
                  .HasColumnName("N_UPDATE_USER_ID");

            entity.Property(e => e.isDeleted)
                  .HasColumnName("N_IS_DELETED");

            entity.Property(e => e.companyId)
                  .HasColumnName("N_COMPANY_ID");

            entity.Property(e => e.documentNumber)
                  .HasColumnName("C_DOCUMENT_NUMBER")
                  .IsRequired();

            entity.Property(e => e.standardId)
                  .HasColumnName("N_STANDARD_ID")
                  .IsRequired();

            // Relaciones FK
            entity.HasOne(e => e.userState)
                  .WithMany()
                  .HasForeignKey(e => e.userStateId)
                  .HasConstraintName("CST_MAE_USER_FK_USERSTATE");

            entity.HasOne(e => e.standard)
                  .WithMany()
                  .HasForeignKey(e => e.standardId)
                  .HasConstraintName("CST_MAE_USER_FK_STANDARD");

        }

    }
}


