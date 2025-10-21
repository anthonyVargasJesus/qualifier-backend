namespace Qualifier.Application.Database.Location.Commands.UpdateLocation
{
    public interface IUpdateLocationCommand
    {
        Task<Object> Execute(UpdateLocationDto model, int id);
    }
}

