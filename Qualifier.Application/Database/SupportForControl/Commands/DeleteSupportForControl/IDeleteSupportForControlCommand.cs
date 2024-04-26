namespace Qualifier.Application.Database.SupportForControl.Commands.DeleteSupportForControl
{
    public interface IDeleteSupportForControlCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

