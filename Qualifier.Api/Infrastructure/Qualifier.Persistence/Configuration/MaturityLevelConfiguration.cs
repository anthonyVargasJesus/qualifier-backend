using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;


namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class MaturityLevelConfiguration
    {
        public MaturityLevelConfiguration(EntityTypeBuilder<MaturityLevelEntity> entityBuilder)
        {
            entityBuilder.ToTable("MAE_MATURITY_LEVEL");

            entityBuilder.HasKey(e => e.maturityLevelId);

            entityBuilder.Property(e => e.maturityLevelId)
                .HasColumnName("N_MATURITY_LEVEL_ID_PK");

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
                .HasColumnName("L_IS_DELETED");

            entityBuilder.Property(e => e.factor)
                .HasColumnName("N_FACTOR");
        }
    }
}
