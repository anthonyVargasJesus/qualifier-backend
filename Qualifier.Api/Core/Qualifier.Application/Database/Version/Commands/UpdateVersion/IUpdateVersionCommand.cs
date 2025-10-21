namespace Qualifier.Application.Database.Version.Commands.UpdateVersion
{
    public interface IUpdateVersionCommand
    {
        Task<Object> Execute(UpdateVersionDto model, int id);
    }
}

