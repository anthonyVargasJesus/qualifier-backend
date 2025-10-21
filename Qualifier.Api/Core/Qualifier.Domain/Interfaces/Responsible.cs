using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IResponsibleRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ResponsibleEntity entity);
    }
}

