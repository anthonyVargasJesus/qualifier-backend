using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class SupportForRequirementConfiguration
    {
        public SupportForRequirementConfiguration(EntityTypeBuilder<SupportForRequirementEntity> entityBuilder)
        {
            //entityBuilder.ToTable("SupportForRequirement");
            //entityBuilder.HasKey(x => x.supportForRequirementId);
            //entityBuilder.Property(x => x.supportForRequirementId).IsRequired();
            //entityBuilder.Property(x => x.documentationId).IsRequired();
            //entityBuilder.Property(x => x.requirementId).IsRequired();
            entityBuilder.ToTable("SUPPORT_FOR_REQUIREMENT");

            entityBuilder.HasKey(e => e.supportForRequirementId);

            entityBuilder.Property(e => e.supportForRequirementId)
                .HasColumnName("N_SUPPORT_FOR_REQUIREMENT_ID_PK")
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn();

            entityBuilder.Property(e => e.documentationId)
                .HasColumnName("N_DOCUMENTATION_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.requirementId)
                .HasColumnName("N_REQUIREMENT_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.companyId)
                .HasColumnName("N_COMPANY_ID_FK");

            entityBuilder.Property(e => e.creationDate)
                .HasColumnName("D_CREATION_DATE");

            entityBuilder.Property(e => e.updateDate)
                .HasColumnName("D_UPDATE_DATE");

            entityBuilder.Property(e => e.creationUserId)
                .HasColumnName("N_CREATION_USER_ID_FK");

            entityBuilder.Property(e => e.updateUserId)
                .HasColumnName("N_UPDATE_USER_ID_FK");

            entityBuilder.Property(e => e.isDeleted)
                .HasColumnName("B_IS_DELETED");

            entityBuilder.Property(e => e.standardId)
                .HasColumnName("N_STANDARD_ID_FK");

        }
    }
}


