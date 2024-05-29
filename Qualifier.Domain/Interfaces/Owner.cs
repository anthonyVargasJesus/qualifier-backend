using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, OwnerEntity entity);
    }
}


