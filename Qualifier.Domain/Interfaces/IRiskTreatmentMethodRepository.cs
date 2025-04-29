using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRiskTreatmentMethodRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RiskTreatmentMethodEntity entity);
    }
}


