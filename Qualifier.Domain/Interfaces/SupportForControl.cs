using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ISupportForControlRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, SupportForControlEntity entity);
    }
}


