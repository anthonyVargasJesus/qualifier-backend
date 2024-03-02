using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IControlGroupRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ControlGroupEntity entity);
    }
}

