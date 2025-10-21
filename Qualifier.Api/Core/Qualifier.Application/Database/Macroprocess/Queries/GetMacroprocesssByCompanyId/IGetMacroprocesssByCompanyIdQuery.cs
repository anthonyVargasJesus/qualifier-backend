namespace Qualifier.Application.Database.Macroprocess.Queries.GetMacroprocesssByCompanyId
{
    public interface IGetMacroprocesssByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

