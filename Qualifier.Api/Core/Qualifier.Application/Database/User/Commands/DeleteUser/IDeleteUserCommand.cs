namespace Qualifier.Application.Database.User.Commands.DeleteUser
{
    public interface IDeleteUserCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

