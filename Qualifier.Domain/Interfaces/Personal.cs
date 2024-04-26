using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IPersonalRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, PersonalEntity entity);
    }
}


