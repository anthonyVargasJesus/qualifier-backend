using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IActiveTypeRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ActiveTypeEntity entity);
    }
}


