using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRiskAssessmentRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RiskAssessmentEntity entity);
    }
}


