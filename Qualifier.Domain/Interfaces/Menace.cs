using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IMenaceRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, MenaceEntity entity);
    }
}


