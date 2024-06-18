namespace Qualifier.Application.Database.UserState.Commands.DeleteUserState
{
    public interface IDeleteUserStateCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

