namespace Qualifier.Application.Database.Macroprocess.Commands.DeleteMacroprocess
{
    public interface IDeleteMacroprocessCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

