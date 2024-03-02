using Qualifier.Application.Database.MaturityLevel.Commands.CreateMaturityLevel;

namespace Qualifier.Application.Database.User.Commands.Login
{
    public interface ILoginUserCommand
    {
        Task<Object> Execute(LoginUserLoginTryDto loginTryDto);
    }
}
