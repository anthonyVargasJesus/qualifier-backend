namespace Qualifier.Application.Database.User.Commands.UpdateUser
{
    public interface IUpdateUserCommand
    {
        Task<Object> Execute(UpdateUserDto model, int id);
    }
}

