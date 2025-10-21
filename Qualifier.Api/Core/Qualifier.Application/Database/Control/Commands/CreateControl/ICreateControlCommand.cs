namespace Qualifier.Application.Database.Control.Commands.CreateControl
{
    public interface ICreateControlCommand
    {
        Task<Object> Execute(CreateControlDto model);
    }
}

