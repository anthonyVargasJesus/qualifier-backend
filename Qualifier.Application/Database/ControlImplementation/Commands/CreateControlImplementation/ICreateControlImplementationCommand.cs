namespace Qualifier.Application.Database.ControlImplementation.Commands.CreateControlImplementation
{
    public interface ICreateControlImplementationCommand
    {
        Task<Object> Execute(CreateControlImplementationDto model);
    }
}

