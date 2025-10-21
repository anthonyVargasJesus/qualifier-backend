using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ISubprocessRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, SubprocessEntity entity);
    }
}


