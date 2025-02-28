using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        protected readonly DatabaseService _context;
        public PolicyRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, PolicyEntity entity)
        {
            entity.policyId = id;
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
            var entity = new PolicyEntity();
            entity.policyId = id;
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
            PolicyEntity entity = new PolicyEntity();
            entity.policyId = id;
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

