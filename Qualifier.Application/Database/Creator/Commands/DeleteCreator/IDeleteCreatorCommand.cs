namespace Qualifier.Application.Database.Creator.Commands.DeleteCreator
{
    public interface IDeleteCreatorCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

