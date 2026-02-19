using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class BreachStatusConfiguration
    {
        public BreachStatusConfiguration(EntityTypeBuilder<BreachStatusEntity> entityBuilder)
        {
            //entityBuilder.ToTable("BreachStatus");
            //entityBuilder.HasKey(x => x.breachStatusId);
            //entityBuilder.Property(x => x.breachStatusId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.description).IsRequired();
            //entityBuilder.Property(x => x.abbreviation).IsRequired();
            //entityBuilder.Property(x => x.value).IsRequired();
            //entityBuilder.Property(x => x.color).IsRequired();
            entityBuilder.ToTable("MAE_BREACH_STATUS");

            entityBuilder.HasKey(e => e.breachStatusId)
                         .HasName("CST_MAE_BREACH_STATUS_PK");

            entityBuilder.Property(e => e.breachStatusId)
                         .HasColumnName("N_BREACH_STATUS_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION")
                         .IsRequired();

            entityBuilder.Property(e => e.abbreviation)
                         .HasColumnName("C_ABBREVIATION")
                         .IsRequired();

            entityBuilder.Property(e => e.value)
                         .HasColumnName("N_VALUE")
                         .IsRequired();

            entityBuilder.Property(e => e.color)
                         .HasColumnName("C_COLOR")
                         .IsRequired();

            entityBuilder.Property(e => e.companyId)
                         .HasColumnName("N_COMPANY_ID_FK");

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
        }
    }
}


