namespace Qualifier.Application.Database.Documentation.Commands.DeleteDocumentation
{
    public interface IDeleteDocumentationCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}


