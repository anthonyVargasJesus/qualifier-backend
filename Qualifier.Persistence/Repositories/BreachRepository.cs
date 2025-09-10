using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class BreachRepository : IBreachRepository
    {
        protected readonly DatabaseService _context;
        public BreachRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, BreachEntity entity)
        {
            entity.breachId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.type).IsModified = true;
            entry.Property(x => x.requirementId).IsModified = true;
            entry.Property(x => x.controlId).IsModified = true;
            entry.Property(x => x.numerationToShow).IsModified = true;
            entry.Property(x => x.title).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.breachSeverityId).IsModified = true;
            entry.Property(x => x.breachStatusId).IsModified = true;
            entry.Property(x => x.responsibleId).IsModified = true;
            entry.Property(x => x.evidenceDescription).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new BreachEntity();
            entity.breachId = id;
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

