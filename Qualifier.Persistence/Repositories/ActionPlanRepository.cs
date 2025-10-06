using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class ActionPlanRepository : IActionPlanRepository
    {
        protected readonly DatabaseService _context;
        public ActionPlanRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, ActionPlanEntity entity)
        {
            entity.actionPlanId = id;
            entity.updateDate = DateTime.UtcNow;

            entity.startDate = DateTime.SpecifyKind(entity.startDate, DateTimeKind.Utc);
            entity.dueDate = DateTime.SpecifyKind(entity.dueDate, DateTimeKind.Utc);
            var entry = _context.Attach(entity);
            entry.Property(x => x.title).IsModified = true;
            entry.Property(x => x.description).IsModified = true;
            entry.Property(x => x.responsibleId).IsModified = true;
            entry.Property(x => x.startDate).IsModified = true;
            entry.Property(x => x.dueDate).IsModified = true;
            entry.Property(x => x.actionPlanStatusId).IsModified = true;
            entry.Property(x => x.actionPlanPriorityId).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new ActionPlanEntity();
            entity.actionPlanId = id;
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

