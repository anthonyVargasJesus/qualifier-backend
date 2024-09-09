
namespace Qualifier.Application.Database.Section.Queries.GetSectionsByDocumentationId
{
    public interface IGetSectionsByDocumentationIdQuery
    {
        Task<Object> Execute(int documentationId);
    }
}
