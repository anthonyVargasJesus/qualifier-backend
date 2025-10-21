using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IScopeRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ScopeEntity entity);
        Task UpdateCurrentState(int id, bool isCurrent);
    }
}


