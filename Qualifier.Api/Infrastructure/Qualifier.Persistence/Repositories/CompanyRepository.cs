using Qualifier.Api.Infrastructure.Qualifier.Persistence.Database;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Infrastructure.Qualifier.Persistence.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        protected readonly DatabaseService _context;
        public CompanyRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task Update(int id, CompanyEntity entity)
        {
            entity.companyId = id;
            entity.updateDate = DateTime.UtcNow;
            var entry = _context.Attach(entity);
            entry.Property(x => x.name).IsModified = true;
            entry.Property(x => x.abbreviation).IsModified = true;
            entry.Property(x => x.slogan).IsModified = true;
            entry.Property(x => x.logo).IsModified = true;
            entry.Property(x => x.address).IsModified = true;
            entry.Property(x => x.phone).IsModified = true;
            entry.Property(x => x.updateDate).IsModified = true;
            entry.Property(x => x.updateUserId).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, int updateUserId)
        {
            var entity = new CompanyEntity();
            entity.companyId = id;
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

