namespace Qualifier.Application.Database.Breach.Commands.CreateBreach
{
    public interface ICreateBreachCommand
    {
        Task<Object> Execute(CreateBreachDto model);
    }
}

