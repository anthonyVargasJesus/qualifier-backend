using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class ControlEvaluationRepository : IControlEvaluationRepository
    {
        protected readonly DatabaseService _context;
        public ControlEvaluationRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, ControlEvaluationEntity entity)
        {
            entity.controlEvaluationId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.controlId).IsModified = true;
            entry.Property(x => x.maturityLevelId).IsModified = true;
            entry.Property(x => x.value).IsModified = true;
            entry.Property(x => x.responsibleId).IsModified = true;
            entry.Property(x => x.justification).IsModified = true;
            entry.Property(x => x.improvementActions).IsModified = true;
            entry.Property(x => x.controlDescription).IsModified = true;
            entry.Property(x => x.controlType).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new ControlEvaluationEntity();
            entity.controlEvaluationId = id;
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

