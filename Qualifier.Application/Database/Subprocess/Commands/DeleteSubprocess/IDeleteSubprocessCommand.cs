namespace Qualifier.Application.Database.Subprocess.Commands.DeleteSubprocess
{
    public interface IDeleteSubprocessCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

