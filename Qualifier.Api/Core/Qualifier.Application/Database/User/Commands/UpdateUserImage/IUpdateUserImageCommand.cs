using Qualifier.Api.Core.Qualifier.Application.Database.User.Commands.UpdateUserImage;
using Qualifier.Application.Database.User.Commands.UpdateUser;

namespace Qualifier.Api.Core.Qualifier.Application.Database.User.Commands.UpdateImage
{
    public interface IUpdateUserImageCommand
    {
        Task<Object> Execute(int id, UpdateUserImageDto user);
    }
}
