namespace Qualifier.Application.Database.ControlType.Commands.CreateControlType
{
    public interface ICreateControlTypeCommand
    {
        Task<Object> Execute(CreateControlTypeDto model);
    }
}

