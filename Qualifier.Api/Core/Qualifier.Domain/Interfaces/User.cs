using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, UserEntity entity);
        Task UpdateLastAccess(int id);
        Task UpdateImage(int id, string image);
    }
}


