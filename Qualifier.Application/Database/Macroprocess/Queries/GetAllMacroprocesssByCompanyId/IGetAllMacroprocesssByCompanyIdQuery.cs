namespace Qualifier.Application.Database.Macroprocess.Queries.GetAllMacroprocesssByCompanyId
{
    public interface IGetAllMacroprocesssByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

