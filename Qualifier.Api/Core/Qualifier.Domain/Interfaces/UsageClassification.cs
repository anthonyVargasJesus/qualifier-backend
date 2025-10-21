using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IUsageClassificationRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, UsageClassificationEntity entity);
    }
}


