namespace Qualifier.Application.Database.Standard.Commands.UpdateStandard
{
public interface IUpdateStandardCommand
{
Task<Object> Execute(UpdateStandardDto model, int id);
}
}

