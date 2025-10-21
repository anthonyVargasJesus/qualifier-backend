namespace Qualifier.Application.Database.Option.Commands.DeleteOption
{
    public interface IDeleteOptionCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

