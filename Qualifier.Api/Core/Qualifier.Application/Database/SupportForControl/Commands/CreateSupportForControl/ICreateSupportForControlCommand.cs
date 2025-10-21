namespace Qualifier.Application.Database.SupportForControl.Commands.CreateSupportForControl
{
    public interface ICreateSupportForControlCommand
    {
        Task<Object> Execute(CreateSupportForControlDto model);
    }
}

