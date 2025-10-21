using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IDefaultRiskRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, DefaultRiskEntity entity);
    }
}


