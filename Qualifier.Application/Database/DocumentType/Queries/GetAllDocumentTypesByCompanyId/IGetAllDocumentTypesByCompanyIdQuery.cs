namespace Qualifier.Application.Database.DocumentType.Queries.GetAllDocumentTypesByCompanyId
{
    public interface IGetAllDocumentTypesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

