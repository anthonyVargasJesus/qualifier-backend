namespace Qualifier.Application.Database.Breach.Commands.UpdateBreach
{
    public interface IUpdateBreachCommand
    {
        Task<Object> Execute(UpdateBreachDto model, int id);
    }
}

