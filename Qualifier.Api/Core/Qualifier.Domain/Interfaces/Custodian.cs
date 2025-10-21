using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ICustodianRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, CustodianEntity entity);
    }
}


