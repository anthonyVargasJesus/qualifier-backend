namespace Qualifier.Application.Database.SupportType.Queries.GetSupportTypesByCompanyId
{
    public interface IGetSupportTypesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}
//services.AddTransient<IGetSupportTypesByCompanyIdQuery, GetSupportTypesByCompanyIdQuery>();
