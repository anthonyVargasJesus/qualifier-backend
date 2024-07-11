namespace Qualifier.Application.Database.ControlType.Commands.UpdateControlType
{
    public interface IUpdateControlTypeCommand
    {
        Task<Object> Execute(UpdateControlTypeDto model, int id);
    }
}

