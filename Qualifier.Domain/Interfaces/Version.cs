using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IVersionRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, VersionEntity entity);
    }
}


