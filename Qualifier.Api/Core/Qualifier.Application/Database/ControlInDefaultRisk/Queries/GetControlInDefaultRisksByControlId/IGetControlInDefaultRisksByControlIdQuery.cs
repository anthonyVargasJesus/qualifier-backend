namespace Qualifier.Application.Database.ControlInDefaultRisk.Queries.GetControlInDefaultRisksByControlId
{
    public interface IGetControlInDefaultRisksByControlIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int controlId);
    }
}

