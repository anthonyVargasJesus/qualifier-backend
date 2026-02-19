using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ControlImplementationConfiguration
    {
        public ControlImplementationConfiguration(EntityTypeBuilder<ControlImplementationEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ControlImplementation");
            //entityBuilder.HasKey(x => x.controlImplementationId);
            //entityBuilder.Property(x => x.controlImplementationId).IsRequired();
            //entityBuilder.Property(x => x.activities).IsRequired();
            //entityBuilder.Property(x => x.startDate).IsRequired();
            //entityBuilder.Property(x => x.responsibleId).IsRequired();
            entityBuilder.ToTable("MAE_CONTROL_IMPLEMENTATION");

            entityBuilder.HasKey(e => e.controlImplementationId)
                .HasName("CST_MAE_CONTROL_IMPLEMENTATION_PK");

            entityBuilder.Property(e => e.controlImplementationId)
                .HasColumnName("N_CONTROL_IMPLEMENTATION_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.riskId)
                .HasColumnName("N_RISK_ID_FK")
                .IsRequired();

            entityBuilder.Property(e => e.activities)
                .HasColumnName("C_ACTIVITIES")
                .IsRequired();

            entityBuilder.Property(e => e.startDate)
                .HasColumnName("D_START_DATE");

            entityBuilder.Property(e => e.verificationDate)
                .HasColumnName("D_VERIFICATION_DATE");

            entityBuilder.Property(e => e.responsibleId)
                .HasColumnName("N_RESPONSIBLE_ID_FK");

            entityBuilder.Property(e => e.observation)
                .HasColumnName("C_OBSERVATION");

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

            entityBuilder.Property(e => e.isImplemented)
                .HasColumnName("L_IS_IMPLEMENTED");

            entityBuilder.Property(e => e.isEffective)
                .HasColumnName("L_IS_EFFECTIVE");
        }
    }
}


