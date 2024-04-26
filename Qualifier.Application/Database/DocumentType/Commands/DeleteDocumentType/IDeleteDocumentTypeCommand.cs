namespace Qualifier.Application.Database.DocumentType.Commands.DeleteDocumentType
{
    public interface IDeleteDocumentTypeCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

