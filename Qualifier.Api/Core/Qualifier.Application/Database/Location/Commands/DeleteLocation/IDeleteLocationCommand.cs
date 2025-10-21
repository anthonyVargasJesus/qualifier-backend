namespace Qualifier.Application.Database.Location.Commands.DeleteLocation
{
    public interface IDeleteLocationCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

