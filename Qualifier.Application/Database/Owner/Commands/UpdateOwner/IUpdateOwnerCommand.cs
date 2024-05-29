namespace Qualifier.Application.Database.Owner.Commands.UpdateOwner
{
    public interface IUpdateOwnerCommand
    {
        Task<Object> Execute(UpdateOwnerDto model, int id);
    }
}

