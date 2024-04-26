namespace Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypeById
{
    public interface IGetDocumentTypeByIdQuery
    {
        Task<Object> Execute(int documentTypeId);
    }
}

