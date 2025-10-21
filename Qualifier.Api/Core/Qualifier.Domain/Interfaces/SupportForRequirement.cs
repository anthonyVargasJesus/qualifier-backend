using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ISupportForRequirementRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, SupportForRequirementEntity entity);
    }
}

