using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class RiskRepository : IRiskRepository
    {
        protected readonly DatabaseService _context;
        public RiskRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, RiskEntity entity)
        {
            entity.riskId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.activesInventoryId).IsModified = true;
            entry.Property(x => x.activesInventoryNumber).IsModified = true;
            entry.Property(x => x.macroProcess).IsModified = true;
            entry.Property(x => x.subProcess).IsModified = true;
            entry.Property(x => x.activesInventoryName).IsModified = true;
            entry.Property(x => x.activesInventoryValuation).IsModified = true;
            entry.Property(x => x.menaceId).IsModified = true;
            entry.Property(x => x.vulnerabilityId).IsModified = true;
            entry.Property(x => x.menaceLevel).IsModified = true;
            entry.Property(x => x.vulnerabilityLevel).IsModified = true;
            entry.Property(x => x.controlId).IsModified = true;
            entry.Property(x => x.riskAssessmentValue).IsModified = true;
            entry.Property(x => x.riskLevelId).IsModified = true;
            entry.Property(x => x.treatmentMethod).IsModified = true;
            entry.Property(x => x.controlTypeId).IsModified = true;
            entry.Property(x => x.controlsToImplement).IsModified = true;
            entry.Property(x => x.menaceLevelWithTreatment).IsModified = true;
            entry.Property(x => x.vulnerabilityLevelWithTreatment).IsModified = true;
            entry.Property(x => x.riskAssessmentValueWithTreatment).IsModified = true;
            entry.Property(x => x.riskLevelWithImplementedControlld).IsModified = true;
            entry.Property(x => x.residualRisk).IsModified = true;
            entry.Property(x => x.activities).IsModified = true;
            entry.Property(x => x.implementationStartDate).IsModified = true;
            entry.Property(x => x.verificationDate).IsModified = true;
            entry.Property(x => x.responsibleId).IsModified = true;
            entry.Property(x => x.observation).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new RiskEntity();
            entity.riskId = id;
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

