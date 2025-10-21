namespace Qualifier.Application.Database.Creator.Commands.CreateCreator
{
    public interface ICreateCreatorCommand
    {
        Task<Object> Execute(CreateCreatorDto model);
    }
}

