namespace Qualifier.Application.Database.Owner.Commands.DeleteOwner
{
    public interface IDeleteOwnerCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

