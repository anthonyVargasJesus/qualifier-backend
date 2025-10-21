namespace Qualifier.Application.Database.Documentation.Queries.GetAllDocumentationsByStandardId
{
    public interface IGetAllDocumentationsByStandardIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

