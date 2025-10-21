using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IControlTypeRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ControlTypeEntity entity);
    }
}


