using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IIndicatorRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, IndicatorEntity entity);
    }
}

