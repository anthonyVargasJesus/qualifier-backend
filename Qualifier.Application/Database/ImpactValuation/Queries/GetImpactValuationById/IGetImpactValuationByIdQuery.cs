namespace Qualifier.Application.Database.ImpactValuation.Queries.GetImpactValuationById
{
    public interface IGetImpactValuationByIdQuery
    {
        Task<Object> Execute(int impactValuationId);
    }
}

