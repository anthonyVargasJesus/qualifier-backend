using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IBreachRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, BreachEntity entity);
    }
}
