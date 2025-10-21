namespace Qualifier.Application.Database.Option.Commands.CreateOption
{
    public interface ICreateOptionCommand
    {
        Task<Object> Execute(CreateOptionDto model);
    }
}

