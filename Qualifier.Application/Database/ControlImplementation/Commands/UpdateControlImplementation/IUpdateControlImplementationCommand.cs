namespace Qualifier.Application.Database.ControlImplementation.Commands.UpdateControlImplementation
{
    public interface IUpdateControlImplementationCommand
    {
        Task<Object> Execute(UpdateControlImplementationDto model, int id);
    }
}

