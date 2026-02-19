using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class ActionPlanStatusConfiguration
    {
        public ActionPlanStatusConfiguration(
            EntityTypeBuilder<ActionPlanStatusEntity> entityBuilder)
        {
            //entityBuilder.ToTable("ActionPlanStatus");
            //entityBuilder.HasKey(x => x.actionPlanStatusId);
            //entityBuilder.Property(x => x.actionPlanStatusId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.description).IsRequired();
            //entityBuilder.Property(x => x.abbreviation).IsRequired();
            //entityBuilder.Property(x => x.value).IsRequired();
            //entityBuilder.Property(x => x.color).IsRequired();
            entityBuilder.ToTable("MAE_ACTION_PLAN_STATUS");

            entityBuilder.HasKey(e => e.actionPlanStatusId);

            entityBuilder.Property(e => e.actionPlanStatusId)
                .HasColumnName("N_ACTION_PLAN_STATUS_ID_PK")
                .ValueGeneratedOnAdd();

            entityBuilder.Property(e => e.name)
                .HasColumnName("C_NAME")
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(e => e.description)
                .HasColumnName("C_DESCRIPTION")
                .HasMaxLength(500)
                .IsRequired();

            entityBuilder.Property(e => e.abbreviation)
                .HasColumnName("C_ABBREVIATION")
                .HasMaxLength(10)
                .IsRequired();

            entityBuilder.Property(e => e.value)
                .HasColumnName("N_VALUE")
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            entityBuilder.Property(e => e.color)
                .HasColumnName("C_COLOR")
                .HasMaxLength(100)
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
                .HasColumnName("N_IS_DELETED");
        }
    }
}


