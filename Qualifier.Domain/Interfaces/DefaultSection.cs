using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IDefaultSectionRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, DefaultSectionEntity entity);
    }
}


