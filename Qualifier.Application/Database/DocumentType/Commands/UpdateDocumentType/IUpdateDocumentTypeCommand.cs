namespace Qualifier.Application.Database.DocumentType.Commands.UpdateDocumentType
{
    public interface IUpdateDocumentTypeCommand
    {
        Task<Object> Execute(UpdateDocumentTypeDto model, int id);
    }
}

