using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IControlInDefaultRiskRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ControlInDefaultRiskEntity entity);
    }
}


