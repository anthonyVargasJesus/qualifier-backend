namespace Qualifier.Application.Database.Version.Queries.GetVersionsByDocumentationId
{
    public interface IGetVersionsByDocumentationIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int documentationId);
    }
}

