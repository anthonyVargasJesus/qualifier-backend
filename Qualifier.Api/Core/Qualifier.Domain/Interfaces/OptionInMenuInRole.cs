using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IOptionInMenuInRoleRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, OptionInMenuInRoleEntity entity);
    }
}


