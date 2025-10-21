namespace Qualifier.Application.Database.ControlType.Commands.DeleteControlType
{
    public interface IDeleteControlTypeCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

