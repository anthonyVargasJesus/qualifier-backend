namespace Qualifier.Application.Database.Version.Commands.DeleteVersion
{
    public interface IDeleteVersionCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

