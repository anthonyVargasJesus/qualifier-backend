using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ISupportTypeRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, SupportTypeEntity entity);
    }
}


