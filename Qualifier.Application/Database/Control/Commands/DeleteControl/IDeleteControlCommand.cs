namespace Qualifier.Application.Database.Control.Commands.DeleteControl
{
    public interface IDeleteControlCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

