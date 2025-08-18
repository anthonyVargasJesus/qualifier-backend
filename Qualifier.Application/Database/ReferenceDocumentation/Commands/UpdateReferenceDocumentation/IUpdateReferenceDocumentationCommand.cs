namespace Qualifier.Application.Database.ReferenceDocumentation.Commands.UpdateReferenceDocumentation
{
    public interface IUpdateReferenceDocumentationCommand
    {
        Task<Object> Execute(UpdateReferenceDocumentationDto model, int id);
    }
}

