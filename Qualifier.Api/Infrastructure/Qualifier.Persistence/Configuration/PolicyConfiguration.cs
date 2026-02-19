using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class PolicyConfiguration
    {
        public PolicyConfiguration(EntityTypeBuilder<PolicyEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Policy");
            //entityBuilder.HasKey(x => x.policyId);
            //entityBuilder.Property(x => x.policyId).IsRequired();
            //entityBuilder.Property(x => x.isCurrent).IsRequired();
            //entityBuilder.Property(x => x.date).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            entityBuilder.ToTable("POL_POLICY");

            entityBuilder.HasKey(e => e.policyId)
                .HasName("CST_POL_POLICY_PK");

            entityBuilder.Property(e => e.policyId)
                .HasColumnName("N_POLICY_ID_PK");

            entityBuilder.Property(e => e.isCurrent)
                .HasColumnName("L_IS_CURRENT")
                .IsRequired();

            entityBuilder.Property(e => e.date)
                .HasColumnName("D_DATE")
                .IsRequired();

            //entityBuilder.Property(e => e.endDate)
            //    .HasColumnName("D_END_DATE");

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(e => e.description)
                .HasColumnName("C_DESCRIPTION")
                .HasMaxLength(500);

            entityBuilder.Property(e => e.standardId)
                .HasColumnName("N_STANDARD_ID_FK");

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
        }
    }
}
