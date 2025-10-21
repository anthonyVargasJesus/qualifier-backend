using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IReferenceDocumentationRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ReferenceDocumentationEntity entity);
    }
}


