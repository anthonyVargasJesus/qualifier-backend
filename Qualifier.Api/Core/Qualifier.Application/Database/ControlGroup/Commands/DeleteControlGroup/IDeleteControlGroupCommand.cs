namespace Qualifier.Application.Database.ControlGroup.Commands.DeleteControlGroup
{
    public interface IDeleteControlGroupCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

