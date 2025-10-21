using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class ControlGroupRepository : IControlGroupRepository
    {
        protected readonly DatabaseService _context;
        public ControlGroupRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, ControlGroupEntity entity)
        {
            entity.controlGroupId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.number).IsModified = true;
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new ControlGroupEntity();
            entity.controlGroupId = id;
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

