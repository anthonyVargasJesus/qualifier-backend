using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class RequirementRepository : IRequirementRepository
    {
        protected readonly DatabaseService _context;
        public RequirementRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, RequirementEntity entity)
        {
            entity.requirementId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.numeration).IsModified = true;
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.letter).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.level).IsModified = true;
            entry.Property(x => x.parentId).IsModified = true;
            entry.Property(x => x.isEvaluable).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new RequirementEntity();
            entity.requirementId = id;
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

