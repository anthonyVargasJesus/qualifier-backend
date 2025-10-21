using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IMaturityLevelRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, MaturityLevelEntity entity);
    }
}

