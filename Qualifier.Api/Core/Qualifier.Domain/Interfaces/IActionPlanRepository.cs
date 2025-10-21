using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IActionPlanRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ActionPlanEntity entity);
    }
}


