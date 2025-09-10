namespace Qualifier.Application.Database.ControlInDefaultRisk.Queries.GetAllControlInDefaultRisksByControlId
{
    public interface IGetAllControlInDefaultRisksByControlIdQuery
    {
        Task<Object> Execute(int controlId);
    }
}

