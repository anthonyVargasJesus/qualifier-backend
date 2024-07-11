using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IMenaceTypeRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, MenaceTypeEntity entity);
    }
}


