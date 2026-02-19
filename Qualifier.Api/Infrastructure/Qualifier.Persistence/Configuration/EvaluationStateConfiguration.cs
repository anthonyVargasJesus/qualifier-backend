using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class EvaluationStateConfiguration
    {
        public EvaluationStateConfiguration(EntityTypeBuilder<EvaluationStateEntity> entityBuilder)
        {
            //entityBuilder.ToTable("EvaluationState");
            //entityBuilder.HasKey(x => x.evaluationStateId);
            //entityBuilder.Property(x => x.evaluationStateId).IsRequired();
            //entityBuilder.Property(x => x.name).IsRequired();
            //entityBuilder.Property(x => x.value).IsRequired();

            entityBuilder.ToTable("MAE_EVALUATION_STATE");

            entityBuilder.HasKey(e => e.evaluationStateId)
                         .HasName("CST_MAE_EVALUATION_STATE_PK");

            entityBuilder.Property(e => e.evaluationStateId)
                         .HasColumnName("N_EVALUATION_STATE_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.name)
                         .HasColumnName("C_NAME")
                         .IsRequired();

            entityBuilder.Property(e => e.value)
                         .HasColumnName("N_VALUE")
                         .IsRequired();

            entityBuilder.Property(e => e.color)
                         .HasColumnName("C_COLOR");

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


