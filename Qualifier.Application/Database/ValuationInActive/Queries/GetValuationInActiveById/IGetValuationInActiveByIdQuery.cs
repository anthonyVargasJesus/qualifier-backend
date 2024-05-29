namespace Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActiveById
{
    public interface IGetValuationInActiveByIdQuery
    {
        Task<Object> Execute(int valuationInActiveId);
    }
}

