namespace Qualifier.Application.Database.ControlGroup.Commands.CreateControlGroup
{
    public interface ICreateControlGroupCommand
    {
        Task<Object> Execute(CreateControlGroupDto model);
    }
}

