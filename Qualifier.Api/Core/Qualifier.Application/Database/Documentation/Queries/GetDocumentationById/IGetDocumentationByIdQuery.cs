namespace Qualifier.Application.Database.Documentation.Queries.GetDocumentationById
{
    public interface IGetDocumentationByIdQuery
    {
        Task<Object> Execute(int documentationId);
    }
}

