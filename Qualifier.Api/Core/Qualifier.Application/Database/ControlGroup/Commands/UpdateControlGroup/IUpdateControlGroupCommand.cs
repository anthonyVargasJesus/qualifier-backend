namespace Qualifier.Application.Database.ControlGroup.Commands.UpdateControlGroup
{
    public interface IUpdateControlGroupCommand
    {
        Task<Object> Execute(UpdateControlGroupDto model, int id);
    }
}

