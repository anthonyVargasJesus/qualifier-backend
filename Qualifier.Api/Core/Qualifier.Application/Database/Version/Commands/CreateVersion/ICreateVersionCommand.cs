namespace Qualifier.Application.Database.Version.Commands.CreateVersion
{
    public interface ICreateVersionCommand
    {
        Task<Object> Execute(CreateVersionDto model);
    }
}

