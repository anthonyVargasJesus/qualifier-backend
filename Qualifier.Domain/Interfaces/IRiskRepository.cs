using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRiskRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RiskEntity entity);
    }
}


