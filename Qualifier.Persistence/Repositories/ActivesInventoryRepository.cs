using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class ActivesInventoryRepository : IActivesInventoryRepository
    {
        protected readonly DatabaseService _context;
        public ActivesInventoryRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, ActivesInventoryEntity entity)
        {
            entity.activesInventoryId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.number).IsModified = true;
            entry.Property(x => x.macroprocessId).IsModified = true;
            entry.Property(x => x.subprocessId).IsModified = true;
            entry.Property(x => x.procedure).IsModified = true;
            entry.Property(x => x.activeTypeId).IsModified = true;
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.ownerId).IsModified = true;
            entry.Property(x => x.custodianId).IsModified = true;
            entry.Property(x => x.usageClassificationId).IsModified = true;
            entry.Property(x => x.supportTypeId).IsModified = true;
            entry.Property(x => x.locationId).IsModified = true;
            entry.Property(x => x.valuation).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new ActivesInventoryEntity();
            entity.activesInventoryId = id;
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

