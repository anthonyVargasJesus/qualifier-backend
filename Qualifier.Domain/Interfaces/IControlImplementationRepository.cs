using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IControlImplementationRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ControlImplementationEntity entity);
    }
}


