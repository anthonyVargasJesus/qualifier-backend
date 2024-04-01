namespace Qualifier.Application.Database.Documentation.Commands.CreateDocumentation
{
    public interface ICreateDocumentationCommand
    {
        Task<Object> Execute(CreateDocumentationDto model);
    }
}

