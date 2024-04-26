namespace Qualifier.Application.Database.DocumentType.Commands.CreateDocumentType
{
    public interface ICreateDocumentTypeCommand
    {
        Task<Object> Execute(CreateDocumentTypeDto model);
    }
}

