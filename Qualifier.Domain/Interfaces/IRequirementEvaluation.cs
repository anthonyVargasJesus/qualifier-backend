using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRequirementEvaluationRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RequirementEvaluationEntity entity);
    }
}


