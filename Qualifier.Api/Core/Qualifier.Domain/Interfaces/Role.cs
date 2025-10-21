using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, RoleEntity entity);
    }
}


