namespace Qualifier.Application.Database.Version.Queries.GetAllVersionsByDocumentationId
{
    public interface IGetAllVersionsByDocumentationIdQuery
    {
        Task<Object> Execute(int documentationId);
    }
}

