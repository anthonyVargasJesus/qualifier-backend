
namespace Qualifier.Application.Database.Section.Queries.GetAllSectionsByDocumentationId
{
    public interface IGetAllSectionsByDocumentationIdQuery
    {
        Task<Object> Execute(int documentationId);
    }
}
