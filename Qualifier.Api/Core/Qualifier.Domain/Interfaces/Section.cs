using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ISectionRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, SectionEntity entity);
    }
}


