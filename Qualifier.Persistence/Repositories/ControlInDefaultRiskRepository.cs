using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class ControlInDefaultRiskRepository : IControlInDefaultRiskRepository
    {
        protected readonly DatabaseService _context;
        public ControlInDefaultRiskRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, ControlInDefaultRiskEntity entity)
        {
            entity.controlInDefaultRiskId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.defaultRiskId).IsModified = true;
            entry.Property(x => x.isActive).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new ControlInDefaultRiskEntity();
            entity.controlInDefaultRiskId = id;
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

