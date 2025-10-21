using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class OptionInMenuInRoleRepository : IOptionInMenuInRoleRepository
    {
        protected readonly DatabaseService _context;
        public OptionInMenuInRoleRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, OptionInMenuInRoleEntity entity)
        {
            entity.optionInMenuInRoleId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.optionId).IsModified = true;
            entry.Property(x => x.order).IsModified = true;
            entry.Property(x => x.menuId).IsModified = true;
            entry.Property(x => x.roleId).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new OptionInMenuInRoleEntity();
            entity.optionInMenuInRoleId = id;
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

