using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class EvaluationRepository : IEvaluationRepository
    {
        protected readonly DatabaseService _context;
        public EvaluationRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task UpdateState(int id, int evaluationStateId)
        {
            EvaluationEntity entity = new EvaluationEntity();
            entity.evaluationId = id;
            entity.evaluationStateId = evaluationStateId;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.evaluationStateId).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCurrentState(int id, bool isCurrent)
        {
            EvaluationEntity entity = new EvaluationEntity();
            entity.evaluationId = id;
            entity.isCurrent = isCurrent;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.isCurrent).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, EvaluationEntity entity)
        {
            entity.evaluationId = id;
            entity.startDate = DateTime.SpecifyKind(entity.startDate, DateTimeKind.Utc);
            entity.endDate = DateTime.SpecifyKind(entity.endDate, DateTimeKind.Utc);
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.startDate).IsModified = true;
            entry.Property(x => x.endDate).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.isGapAnalysis).IsModified = true;
            entry.Property(x => x.isCurrent).IsModified = true;
            entry.Property(x => x.referenceEvaluationId).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new EvaluationEntity();
            entity.evaluationId = id;
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

