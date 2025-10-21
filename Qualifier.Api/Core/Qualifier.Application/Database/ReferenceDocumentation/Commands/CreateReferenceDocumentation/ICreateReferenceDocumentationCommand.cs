namespace Qualifier.Application.Database.ReferenceDocumentation.Commands.CreateReferenceDocumentation
{
    public interface ICreateReferenceDocumentationCommand
    {
        Task<Object> Execute(CreateReferenceDocumentationDto model);
    }
}

