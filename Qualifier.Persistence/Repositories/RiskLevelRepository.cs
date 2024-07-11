using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class RiskLevelRepository : IRiskLevelRepository
    {
        protected readonly DatabaseService _context;
        public RiskLevelRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, RiskLevelEntity entity)
        {
            entity.riskLevelId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.abbreviation).IsModified = true;
            entry.Property(x => x.factor).IsModified = true;
            entry.Property(x => x.minimum).IsModified = true;
            entry.Property(x => x.maximum).IsModified = true;
            entry.Property(x => x.color).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new RiskLevelEntity();
            entity.riskLevelId = id;
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

