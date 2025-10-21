namespace Qualifier.Application.Database.Indicator.Queries.GetIndicatorsByCompanyId
{
    public interface IGetIndicatorsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

