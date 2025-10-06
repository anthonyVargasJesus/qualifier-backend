using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IActionPlanStatusRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ActionPlanStatusEntity entity);
    }
}


