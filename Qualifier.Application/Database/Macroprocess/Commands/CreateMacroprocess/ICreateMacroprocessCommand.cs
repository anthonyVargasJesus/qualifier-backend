namespace Qualifier.Application.Database.Macroprocess.Commands.CreateMacroprocess
{
    public interface ICreateMacroprocessCommand
    {
        Task<Object> Execute(CreateMacroprocessDto model);
    }
}

