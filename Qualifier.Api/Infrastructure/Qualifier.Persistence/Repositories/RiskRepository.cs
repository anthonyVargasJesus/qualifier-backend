using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class RiskRepository : IRiskRepository
    {
        protected readonly DatabaseService _context;
        public RiskRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, RiskEntity entity)
        {
            entity.riskId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.activesInventoryId).IsModified = true;
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.activesInventoryNumber).IsModified = true;
            entry.Property(x => x.activesInventoryName).IsModified = true;
            entry.Property(x => x.menaceId).IsModified = true;
            entry.Property(x => x.vulnerabilityId).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRiskStatusId(int id, int riskStatusId, int? updateUserId)
        {
            var entity = new RiskEntity();
            entity.riskId = id;
            entity.updateDate = DateTime.UtcNow;
            entity.updateUserId = updateUserId;
            entity.riskStatusId = riskStatusId;
            var entry = _context.Attach(entity);
            entry.Property(x => x.riskStatusId).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new RiskEntity();
            entity.riskId = id;
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

