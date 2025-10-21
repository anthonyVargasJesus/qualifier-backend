namespace Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByCompanyId
{
    public interface IGetDocumentationsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

