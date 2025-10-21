using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRoleInUserRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RoleInUserEntity entity);
    }
}

