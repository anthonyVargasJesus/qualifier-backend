namespace Qualifier.Application.Database.Owner.Commands.CreateOwner
{
    public interface ICreateOwnerCommand
    {
        Task<Object> Execute(CreateOwnerDto model);
    }
}

