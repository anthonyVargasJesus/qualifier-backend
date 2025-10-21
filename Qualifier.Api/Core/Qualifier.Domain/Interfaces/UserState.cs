using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IUserStateRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, UserStateEntity entity);
    }
}


