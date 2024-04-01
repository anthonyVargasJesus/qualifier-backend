using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IDocumentationRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, DocumentationEntity entity);
    }
}

