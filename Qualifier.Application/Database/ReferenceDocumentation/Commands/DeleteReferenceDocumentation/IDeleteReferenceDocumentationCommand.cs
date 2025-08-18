namespace Qualifier.Application.Database.ReferenceDocumentation.Commands.DeleteReferenceDocumentation
{
    public interface IDeleteReferenceDocumentationCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

