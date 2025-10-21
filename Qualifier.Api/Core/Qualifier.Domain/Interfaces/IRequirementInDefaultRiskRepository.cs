using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRequirementInDefaultRiskRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RequirementInDefaultRiskEntity entity);
    }
}
