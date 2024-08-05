using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IEvaluationRepository
    {
        Task UpdateCurrentState(int id, bool isCurrent);
        Task Delete(int id, int updateUserId);
        Task Update(int id, EvaluationEntity entity);
        Task UpdateState(int id, int evaluationStateId);
    }
}

