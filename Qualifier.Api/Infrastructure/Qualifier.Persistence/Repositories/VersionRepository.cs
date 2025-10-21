using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class VersionRepository : IVersionRepository
    {
        protected readonly DatabaseService _context;
        public VersionRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, VersionEntity entity)
        {
            entity.versionId = id;

            entity.date = DateTime.SpecifyKind(entity.date, DateTimeKind.Utc);

            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.number).IsModified = true;
            entry.Property(x => x.code).IsModified = true;
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.confidentialityLevelId).IsModified = true;
            entry.Property(x => x.documentationId).IsModified = true;
            entry.Property(x => x.date).IsModified = true;
            entry.Property(x => x.isCurrent).IsModified = true;
            entry.Property(x => x.fileName).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new VersionEntity();
            entity.versionId = id;
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

