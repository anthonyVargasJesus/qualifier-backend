using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRiskLevelRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RiskLevelEntity entity);
    }
}


