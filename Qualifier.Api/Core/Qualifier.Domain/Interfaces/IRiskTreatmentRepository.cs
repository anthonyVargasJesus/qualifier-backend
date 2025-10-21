using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRiskTreatmentRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RiskTreatmentEntity entity);
    }
}


