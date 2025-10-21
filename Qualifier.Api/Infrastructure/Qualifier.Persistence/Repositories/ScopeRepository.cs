using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class ScopeRepository : IScopeRepository
    {
        protected readonly DatabaseService _context;
        public ScopeRepository(DatabaseService context)
        {
            _context = context;
        }
        public async Task Update(int id, ScopeEntity entity)
        {
            entity.scopeId = id;
            entity.date = DateTime.SpecifyKind(entity.date, DateTimeKind.Utc);
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.isCurrent).IsModified = true;
            entry.Property(x => x.date).IsModified = true;
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new ScopeEntity();
            entity.scopeId = id;
            entity.updateDate = DateTime.UtcNow;
            entity.updateUserId = updateUserId;
            entity.isDeleted = true;
            var entry = _context.Attach(entity);
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            entry.Property(x => x.isDeleted).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCurrentState(int id, bool isCurrent)
        {
            ScopeEntity entity = new ScopeEntity();
            entity.scopeId = id;
            entity.isCurrent = isCurrent;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.isCurrent).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

    }
}

