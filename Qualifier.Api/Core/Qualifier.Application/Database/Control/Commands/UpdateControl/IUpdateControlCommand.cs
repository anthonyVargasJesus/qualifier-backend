namespace Qualifier.Application.Database.Control.Commands.UpdateControl
{
public interface IUpdateControlCommand
{
Task<Object> Execute(UpdateControlDto model, int id);
}
}

