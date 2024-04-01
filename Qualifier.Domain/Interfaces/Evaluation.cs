using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IEvaluationRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, EvaluationEntity entity);
    }
}

