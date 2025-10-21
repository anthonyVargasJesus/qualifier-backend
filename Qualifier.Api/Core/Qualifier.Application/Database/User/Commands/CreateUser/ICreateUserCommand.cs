namespace Qualifier.Application.Database.User.Commands.CreateUser
{
    public interface ICreateUserCommand
    {
        Task<Object> Execute(CreateUserDto model);
    }
}

