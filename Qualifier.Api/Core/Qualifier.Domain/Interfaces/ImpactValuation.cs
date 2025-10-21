using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IImpactValuationRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ImpactValuationEntity entity);
    }
}


