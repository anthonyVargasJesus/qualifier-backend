using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ICreatorRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, CreatorEntity entity);
    }
}


