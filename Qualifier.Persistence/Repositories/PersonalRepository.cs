using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;

namespace Qualifier.Persistence.Repositories
{
    public class PersonalRepository : IPersonalRepository
    {
        protected readonly DatabaseService _context;
        public PersonalRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, PersonalEntity entity)
        {
            entity.personalId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.middleName).IsModified = true;
            entry.Property(x => x.firstName).IsModified = true;
            entry.Property(x => x.lastName).IsModified = true;
            entry.Property(x => x.email).IsModified = true;
            entry.Property(x => x.phone).IsModified = true;
            entry.Property(x => x.position).IsModified = true;
            entry.Property(x => x.image).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new PersonalEntity();
            entity.personalId = id;
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

