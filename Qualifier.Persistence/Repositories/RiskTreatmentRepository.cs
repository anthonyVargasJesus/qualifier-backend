using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class RiskTreatmentRepository : IRiskTreatmentRepository
    {
        protected readonly DatabaseService _context;
        public RiskTreatmentRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, RiskTreatmentEntity entity)
        {
            entity.riskTreatmentId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.riskTreatmentMethodId).IsModified = true;
            entry.Property(x => x.controlType).IsModified = true;
            entry.Property(x => x.controlsToImplement).IsModified = true;
            entry.Property(x => x.menaceLevelValue).IsModified = true;
            entry.Property(x => x.vulnerabilityLevelValue).IsModified = true;
            entry.Property(x => x.riskAssessmentValue).IsModified = true;
            entry.Property(x => x.riskLevelId).IsModified = true;
            entry.Property(x => x.residualRiskId).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new RiskTreatmentEntity();
            entity.riskTreatmentId = id;
            entity.updateDate = DateTime.UtcNow;
            entity.updateUserId = updateUserId;
            entity.isDeleted = true;
            var entry = _context.Attach(entity);
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            entry.Property(x => x.isDeleted).IsModified = true;
            await _context.SaveChangesAsync();
        }

    }
}

