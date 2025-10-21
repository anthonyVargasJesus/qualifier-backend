using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class ImpactValuationRepository : IImpactValuationRepository
    {
        protected readonly DatabaseService _context;
        public ImpactValuationRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, ImpactValuationEntity entity)
        {
            entity.impactValuationId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.abbreviation).IsModified = true;
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.minimumValue).IsModified = true;
            entry.Property(x => x.maximumValue).IsModified = true;
            entry.Property(x => x.defaultValue).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new ImpactValuationEntity();
            entity.impactValuationId = id;
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

