namespace Qualifier.Application.Database.ControlImplementation.Queries.GetControlImplementationsByRiskId
{
    public interface IGetControlImplementationsByRiskIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int riskId);
    }
}

