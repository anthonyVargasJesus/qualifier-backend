using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRequirementRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RequirementEntity entity);
    }
}

