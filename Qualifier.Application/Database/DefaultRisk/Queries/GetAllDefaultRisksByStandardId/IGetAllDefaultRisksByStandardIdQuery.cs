namespace Qualifier.Application.Database.DefaultRisk.Queries.GetAllDefaultRisksByStandardId
{
    public interface IGetAllDefaultRisksByStandardIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

