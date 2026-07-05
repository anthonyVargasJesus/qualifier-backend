using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class UserRequirementFamilyConfiguration
    {
        public UserRequirementFamilyConfiguration(EntityTypeBuilder<UserRequirementFamilyEntity> entityBuilder)
        {
            entityBuilder.ToTable("MAE_USER_REQUIREMENT_FAMILY");

            entityBuilder.HasKey(e => e.userRequirementFamilyId)
                         .HasName("CST_MAE_USER_REQUIREMENT_FAMILY_PK");

            entityBuilder.Property(e => e.userRequirementFamilyId)
                         .HasColumnName("N_USER_REQUIREMENT_FAMILY_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.userId)
                         .HasColumnName("N_USER_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.requirementId)
                         .HasColumnName("N_REQUIREMENT_ID_FK")
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
            entityBuilder.HasOne(e => e.requirement).WithMany().HasForeignKey(e => e.requirementId);
        }
    }
}
