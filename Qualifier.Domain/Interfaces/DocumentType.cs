using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IDocumentTypeRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, DocumentTypeEntity entity);
    }
}


