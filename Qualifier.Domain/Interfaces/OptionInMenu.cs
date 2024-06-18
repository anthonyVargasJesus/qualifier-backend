using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IOptionInMenuRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, OptionInMenuEntity entity);
    }
}


