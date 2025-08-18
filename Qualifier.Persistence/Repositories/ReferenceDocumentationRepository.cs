using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class ReferenceDocumentationRepository : IReferenceDocumentationRepository
    {
        protected readonly DatabaseService _context;
        public ReferenceDocumentationRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, ReferenceDocumentationEntity entity)
        {
            entity.referenceDocumentationId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.url).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.documentationId).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new ReferenceDocumentationEntity();
            entity.referenceDocumentationId = id;
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

