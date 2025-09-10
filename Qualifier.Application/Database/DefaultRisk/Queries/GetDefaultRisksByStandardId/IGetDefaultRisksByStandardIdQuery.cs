namespace Qualifier.Application.Database.DefaultRisk.Queries.GetDefaultRisksByStandardId
{
    public interface IGetDefaultRisksByStandardIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int standardId);
    }
}

