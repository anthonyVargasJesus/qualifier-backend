using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class StandardConfiguration
    {
        public StandardConfiguration(EntityTypeBuilder<StandardEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Standard");
            //entityBuilder.HasKey(x => x.standardId);
            //entityBuilder.Property(x => x.standardId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.description).IsRequired();

            entityBuilder.ToTable("MAE_STANDARD");

            entityBuilder.HasKey(e => e.standardId)
                         .HasName("CST_MAE_STANDARD_PK");

            entityBuilder.Property(e => e.standardId)
                         .HasColumnName("N_STANDARD_ID_PK")
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .HasMaxLength(100)
                         .IsRequired();

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION")
                         .HasMaxLength(500)
                         .IsRequired();

            entityBuilder.Property(e => e.parentId)
                         .HasColumnName("N_PARENT_ID");

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

            entityBuilder.HasOne(x => x.standard)
            .WithMany(x => x.standards)
            .HasForeignKey(x => x.parentId);



        }
    }
}


