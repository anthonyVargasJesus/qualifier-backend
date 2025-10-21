namespace Qualifier.Application.Database.SupportType.Queries.GetAllSupportTypesByCompanyId
{
    public interface IGetAllSupportTypesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}
//services.AddTransient<IGetAllSupportTypesByCompanyIdQuery, GetAllSupportTypesByCompanyIdQuery>();
