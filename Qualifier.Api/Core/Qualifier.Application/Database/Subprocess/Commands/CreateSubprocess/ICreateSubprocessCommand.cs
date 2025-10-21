namespace Qualifier.Application.Database.Subprocess.Commands.CreateSubprocess
{
    public interface ICreateSubprocessCommand
    {
        Task<Object> Execute(CreateSubprocessDto model);
    }
}

