namespace Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByStandardId
{
    public interface IGetDocumentationsByStandardIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int requirementId);
    }
}

