using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IActivesInventoryRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ActivesInventoryEntity entity);
    }
}


