using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class RiskAssessmentRepository : IRiskAssessmentRepository
    {
        protected readonly DatabaseService _context;
        public RiskAssessmentRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, RiskAssessmentEntity entity)
        {
            entity.riskAssessmentId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.valuationCID).IsModified = true;
            entry.Property(x => x.menaceLevelValue).IsModified = true;
            entry.Property(x => x.vulnerabilityLevelValue).IsModified = true;
            entry.Property(x => x.existingImplementedControls).IsModified = true;
            entry.Property(x => x.riskAssessmentValue).IsModified = true;
            entry.Property(x => x.riskLevelId).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new RiskAssessmentEntity();
            entity.riskAssessmentId = id;
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

