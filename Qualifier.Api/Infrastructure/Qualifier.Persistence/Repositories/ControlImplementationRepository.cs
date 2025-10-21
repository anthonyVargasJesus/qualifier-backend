using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class ControlImplementationRepository : IControlImplementationRepository
    {
        protected readonly DatabaseService _context;
        public ControlImplementationRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, ControlImplementationEntity entity)
        {
            entity.controlImplementationId = id;
            entity.updateDate = DateTime.UtcNow;

            entity.startDate = DateTime.SpecifyKind(entity.startDate, DateTimeKind.Utc);

            if (entity.verificationDate.HasValue)
                entity.verificationDate = DateTime
                    .SpecifyKind(entity.verificationDate.Value, DateTimeKind.Utc);
            else
                entity.verificationDate = null;

            var entry = _context.Attach(entity);
            entry.Property(x => x.activities).IsModified = true;
            entry.Property(x => x.startDate).IsModified = true;
            entry.Property(x => x.verificationDate).IsModified = true;
            entry.Property(x => x.responsibleId).IsModified = true;
            entry.Property(x => x.observation).IsModified = true;
            entry.Property(x => x.isEffective).IsModified = true;
            entry.Property(x => x.isImplemented).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id, int updateUserId)
        {
            var entity = new ControlImplementationEntity();
            entity.controlImplementationId = id;
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

