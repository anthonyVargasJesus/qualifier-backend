using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class SupportForControlConfiguration
    {
        public SupportForControlConfiguration(EntityTypeBuilder<SupportForControlEntity> entityBuilder)
        {
            //entityBuilder.ToTable("SupportForControl");
            //entityBuilder.HasKey(x => x.supportForControlId);
            //entityBuilder.Property(x => x.supportForControlId).IsRequired();
            //entityBuilder.Property(x => x.documentationId).IsRequired();
            //entityBuilder.Property(x => x.controlId).IsRequired();
            entityBuilder.ToTable("SUPPORT_FOR_CONTROL");

            entityBuilder.HasKey(e => e.supportForControlId);

            entityBuilder.Property(e => e.supportForControlId)
                .HasColumnName("N_SUPPORT_FOR_CONTROL_ID_PK")
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn();

            entityBuilder.Property(e => e.documentationId)
                .HasColumnName("N_DOCUMENTATION_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.controlId)
                .HasColumnName("N_CONTROL_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.standardId)
                .HasColumnName("N_STANDARD_ID_FK");

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

        }
    }
}

