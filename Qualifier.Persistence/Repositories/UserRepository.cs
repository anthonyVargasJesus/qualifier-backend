using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly DatabaseService _context;
        public UserRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, UserEntity entity)
        {
            entity.userId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.middleName).IsModified = true;
            entry.Property(x => x.firstName).IsModified = true;
            entry.Property(x => x.lastName).IsModified = true;
            entry.Property(x => x.email).IsModified = true;
            entry.Property(x => x.phone).IsModified = true;
            entry.Property(x => x.userStateId).IsModified = true;
            entry.Property(x => x.standardId).IsModified = true;
            entry.Property(x => x.documentNumber).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new UserEntity();
            entity.userId = id;
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

