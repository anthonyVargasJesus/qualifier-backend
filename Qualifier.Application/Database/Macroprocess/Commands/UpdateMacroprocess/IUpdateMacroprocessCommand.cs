namespace Qualifier.Application.Database.Macroprocess.Commands.UpdateMacroprocess
{
    public interface IUpdateMacroprocessCommand
    {
        Task<Object> Execute(UpdateMacroprocessDto model, int id);
    }
}

