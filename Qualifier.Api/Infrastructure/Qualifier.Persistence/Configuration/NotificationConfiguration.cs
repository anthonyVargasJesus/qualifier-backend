using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class NotificationConfiguration
    {
        public NotificationConfiguration(EntityTypeBuilder<NotificationEntity> entityBuilder)
        {
            entityBuilder.ToTable("MAE_NOTIFICATION");

            entityBuilder.HasKey(e => e.notificationId)
                         .HasName("CST_MAE_NOTIFICATION_PK");

            entityBuilder.Property(e => e.notificationId)
                         .HasColumnName("N_NOTIFICATION_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.userId)
                         .HasColumnName("N_USER_ID_FK")
                         .IsRequired();

            entityBuilder.Property(e => e.title)
                         .HasColumnName("C_TITLE")
                         .IsRequired();

            entityBuilder.Property(e => e.body)
                         .HasColumnName("C_BODY")
                         .IsRequired();

            entityBuilder.Property(e => e.type)
                         .HasColumnName("C_TYPE");

            entityBuilder.Property(e => e.actionPlanId)
                         .HasColumnName("N_ACTION_PLAN_ID_FK");

            entityBuilder.Property(e => e.breachId)
                         .HasColumnName("N_BREACH_ID_FK");

            entityBuilder.Property(e => e.companyId)
                         .HasColumnName("N_COMPANY_ID_FK");

            entityBuilder.Property(e => e.isRead)
                         .HasColumnName("N_IS_READ");

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
