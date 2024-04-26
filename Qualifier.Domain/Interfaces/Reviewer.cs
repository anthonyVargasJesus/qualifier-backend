using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IReviewerRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ReviewerEntity entity);
    }
}


