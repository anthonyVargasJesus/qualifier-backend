using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ILocationRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, LocationEntity entity);
    }
}


