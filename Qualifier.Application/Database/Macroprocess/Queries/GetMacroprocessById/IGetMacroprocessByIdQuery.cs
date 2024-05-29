namespace Qualifier.Application.Database.Macroprocess.Queries.GetMacroprocessById
{
    public interface IGetMacroprocessByIdQuery
    {
        Task<Object> Execute(int macroprocessId);
    }
}

