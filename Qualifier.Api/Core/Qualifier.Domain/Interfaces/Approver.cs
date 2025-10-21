using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IApproverRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ApproverEntity entity);
    }
}


