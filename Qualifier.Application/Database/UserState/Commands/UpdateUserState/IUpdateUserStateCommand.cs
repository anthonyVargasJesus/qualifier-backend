namespace Qualifier.Application.Database.UserState.Commands.UpdateUserState
{
    public interface IUpdateUserStateCommand
    {
        Task<Object> Execute(UpdateUserStateDto model, int id);
    }
}

