using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qualifier.Domain.Entities;

namespace Qualifier.Persistence.Configuration
{
    public class ApproverConfiguration
    {
        public ApproverConfiguration(EntityTypeBuilder<ApproverEntity> entityBuilder)
        {
            entityBuilder.ToTable("Approver");
            entityBuilder.HasKey(x => x.approverId);
            entityBuilder.Property(x => x.approverId).IsRequired();
            entityBuilder.Property(x => x.versionId).IsRequired();

            //entityBuilder.HasOne(x => x.personal)
            //.WithMany(x => x.approvers)
            //.HasForeignKey(x => x.personalId);

            //entityBuilder.HasOne(x => x.responsible)
            //.WithMany(x => x.approvers)
            //.HasForeignKey(x => x.responsibleId);

            //entityBuilder.HasOne(x => x.version)
            //.WithMany(x => x.approvers)
            //.HasForeignKey(x => x.versionId);
        }
    }
}


