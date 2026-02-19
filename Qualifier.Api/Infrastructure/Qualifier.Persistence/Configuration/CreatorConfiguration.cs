using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class CreatorConfiguration
    {
        public CreatorConfiguration(EntityTypeBuilder<CreatorEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Creator");
            //entityBuilder.HasKey(x => x.creatorId);
            //entityBuilder.Property(x => x.creatorId).IsRequired();
            //entityBuilder.Property(x => x.versionId).IsRequired();
            entityBuilder.ToTable("MAE_CREATOR");

            entityBuilder.HasKey(e => e.creatorId)
                         .HasName("CST_MAE_CREATOR_PK");

            entityBuilder.Property(e => e.creatorId)
                         .HasColumnName("N_CREATOR_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.personalId)
                         .HasColumnName("N_PERSONAL_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.responsibleId)
                         .HasColumnName("N_RESPONSIBLE_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.versionId)
                         .HasColumnName("N_VERSION_ID_FK");

            entityBuilder.Property(e => e.documentationId)
                         .HasColumnName("N_DOCUMENTATION_ID_FK");

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


