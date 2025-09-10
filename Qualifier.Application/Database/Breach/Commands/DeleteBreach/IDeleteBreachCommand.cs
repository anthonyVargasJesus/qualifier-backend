namespace Qualifier.Application.Database.Breach.Commands.DeleteBreach
{
    public interface IDeleteBreachCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

