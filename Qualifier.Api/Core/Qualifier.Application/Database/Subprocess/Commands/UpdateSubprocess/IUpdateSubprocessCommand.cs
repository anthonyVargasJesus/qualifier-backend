namespace Qualifier.Application.Database.Subprocess.Commands.UpdateSubprocess
{
    public interface IUpdateSubprocessCommand
    {
        Task<Object> Execute(UpdateSubprocessDto model, int id);
    }
}

