using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IValuationInActiveRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ValuationInActiveEntity entity);
    }
}


