namespace Qualifier.Application.Database.Documentation.Commands.UpdateDocumentation
{
    public interface IUpdateDocumentationCommand
    {
        Task<Object> Execute(UpdateDocumentationDto model, int id);
    }
}

