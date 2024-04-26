namespace Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypesByCompanyId
{
    public interface IGetDocumentTypesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

