using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IMenuInRoleRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, MenuInRoleEntity entity);
    }
}


