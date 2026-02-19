using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Configuration
{
    public class EvaluationConfiguration
    {
        public EvaluationConfiguration(EntityTypeBuilder<EvaluationEntity> entityBuilder)
        {
            //entityBuilder.ToTable("Evaluation");
            //entityBuilder.HasKey(x => x.evaluationId);
            //entityBuilder.Property(x => x.evaluationId).IsRequired();
            //entityBuilder.Property(x => x.startDate).IsRequired();
            //entityBuilder.Property(x => x.evaluationStateId).IsRequired();
            //entityBuilder.HasOne(x => x.standard)
            //.WithMany(x => x.evaluations)
            //.HasForeignKey(x => x.standardId);

            entityBuilder.ToTable("MAE_EVALUATION");

            entityBuilder.HasKey(e => e.evaluationId)
                         .HasName("CST_MAE_EVALUATION_PK");

            entityBuilder.Property(e => e.evaluationId)
                         .HasColumnName("N_EVALUATION_ID_PK")
                         .ValueGeneratedOnAdd()
                         .IsRequired();

            entityBuilder.Property(e => e.startDate)
                         .HasColumnName("D_START_DATE")
                         .IsRequired();

            entityBuilder.Property(e => e.endDate)
                         .HasColumnName("D_END_DATE");

            entityBuilder.Property(e => e.description)
                         .HasColumnName("C_DESCRIPTION");

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
                         .HasColumnName("N_IS_DELETED");

            entityBuilder.Property(e => e.evaluationStateId)
                         .HasColumnName("N_EVALUATION_STATE_ID_FK");

            entityBuilder.Property(e => e.referenceEvaluationId)
                         .HasColumnName("N_REFERENCE_EVALUATION_ID");

            entityBuilder.Property(e => e.isGapAnalysis)
                         .HasColumnName("N_IS_GAP_ANALYSIS");

            entityBuilder.Property(e => e.isCurrent)
                         .HasColumnName("N_IS_CURRENT")
                         .IsRequired();

        }
    }
}


