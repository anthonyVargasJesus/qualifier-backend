namespace Qualifier.Application.Database.UserState.Commands.CreateUserState
{
    public interface ICreateUserStateCommand
    {
        Task<Object> Execute(CreateUserStateDto model);
    }
}

