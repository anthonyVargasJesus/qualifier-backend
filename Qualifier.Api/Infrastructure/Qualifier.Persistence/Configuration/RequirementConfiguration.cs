using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class RequirementConfiguration
    {
        public RequirementConfiguration(EntityTypeBuilder<RequirementEntity> entityBuilder)
        {
            entityBuilder.ToTable("MAE_REQUIREMENT");

            entityBuilder.HasKey(e => e.requirementId)
                         .HasName("CST_MAE_REQUIREMENT_PK");

            entityBuilder.Property(e => e.requirementId)
                         .HasColumnName("N_REQUIREMENT_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.numeration)
                         .HasColumnName("N_NUMERATION")
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .HasMaxLength(500)
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION")
                         .HasMaxLength(1000);

            entityBuilder.Property(e => e.standardId)
                         .HasColumnName("N_STANDARD_ID");

            entityBuilder.Property(e => e.level)
                         .HasColumnName("N_LEVEL")
                         .IsRequired();

            entityBuilder.Property(e => e.companyId)
                         .HasColumnName("N_COMPANY_ID");

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

            entityBuilder.Property(e => e.parentId)
                         .HasColumnName("N_PARENT_ID");

            entityBuilder.Property(e => e.isEvaluable)
                         .HasColumnName("N_IS_EVALUABLE");

            entityBuilder.Property(e => e.letter)
                         .HasColumnName("C_LETTER")
                         .HasMaxLength(10);

            entityBuilder.HasOne(x => x.requirement)
            .WithMany(x => x.requirements)
            .HasForeignKey(x => x.parentId);
        }
    }
}


