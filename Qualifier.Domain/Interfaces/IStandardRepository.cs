using Qualifier.Domain.Entities;


namespace Qualifier.Domain.Interfaces
{
    public interface IStandardRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, StandardEntity entity);
    }
}
