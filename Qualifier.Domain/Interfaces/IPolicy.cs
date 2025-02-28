using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IPolicyRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, PolicyEntity entity);
        Task UpdateCurrentState(int id, bool isCurrent);
    }
}


