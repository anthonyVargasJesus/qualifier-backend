namespace Qualifier.Application.Database.Creator.Commands.UpdateCreator
{
    public interface IUpdateCreatorCommand
    {
        Task<Object> Execute(UpdateCreatorDto model, int id);
    }
}

