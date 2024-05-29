namespace Qualifier.Application.Database.Location.Commands.CreateLocation
{
    public interface ICreateLocationCommand
    {
        Task<Object> Execute(CreateLocationDto model);
    }
}

