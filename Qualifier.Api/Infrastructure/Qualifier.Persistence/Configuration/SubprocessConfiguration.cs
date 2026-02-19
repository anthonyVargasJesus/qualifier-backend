using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class SubprocessConfiguration
    {
        public SubprocessConfiguration(EntityTypeBuilder<SubprocessEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Subprocess");
            //entityBuilder.HasKey(x => x.subprocessId);
            //entityBuilder.Property(x => x.subprocessId).IsRequired();
            //entityBuilder.Property(x => x.code).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.macroprocessId).IsRequired();
            entityBuilder.ToTable("MAE_SUBPROCESS");

            entityBuilder.HasKey(e => e.subprocessId)
                .HasName("CST_MAE_SUBPROCESS_PK");

            entityBuilder.Property(e => e.subprocessId)
                .HasColumnName("N_SUBPROCESS_ID_PK")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entityBuilder.Property(e => e.code)
                .HasColumnName("C_CODE")
                .IsRequired();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .IsRequired();

            entityBuilder.Property(e => e.macroprocessId)
                .HasColumnName("N_MACROPROCESS_ID_FK");

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


