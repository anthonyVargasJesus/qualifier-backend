namespace Qualifier.Application.Database.SupportForControl.Commands.UpdateSupportForControl
{
    public interface IUpdateSupportForControlCommand
    {
        Task<Object> Execute(UpdateSupportForControlDto model, int id);
    }
}

