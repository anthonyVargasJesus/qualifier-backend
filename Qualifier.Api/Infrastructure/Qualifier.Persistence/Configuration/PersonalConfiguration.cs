using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class PersonalConfiguration
    {
        public PersonalConfiguration(EntityTypeBuilder<PersonalEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Personal");
            //entityBuilder.HasKey(x => x.personalId);
            //entityBuilder.Property(x => x.personalId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.firstName).IsRequired();
            entityBuilder.ToTable("MAE_PERSONAL");

            entityBuilder.HasKey(e => e.personalId)
                         .HasName("CST_MAE_PERSONAL_PK");

            entityBuilder.Property(e => e.personalId)
                         .HasColumnName("N_PERSONAL_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME");

            entityBuilder.Property(e => e.middleName)
                         .HasColumnName("C_MIDDLE_NAME");

            entityBuilder.Property(e => e.firstName)
                         .HasColumnName("C_FIRST_NAME");

            entityBuilder.Property(e => e.lastName)
                         .HasColumnName("C_LAST_NAME");

            entityBuilder.Property(e => e.email)
                         .HasColumnName("C_EMAIL");

            entityBuilder.Property(e => e.phone)
                         .HasColumnName("C_PHONE");

            entityBuilder.Property(e => e.position)
                         .HasColumnName("C_POSITION");

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
                         .HasColumnName("N_COMPANY_ID_FK");
        }
    }
}


