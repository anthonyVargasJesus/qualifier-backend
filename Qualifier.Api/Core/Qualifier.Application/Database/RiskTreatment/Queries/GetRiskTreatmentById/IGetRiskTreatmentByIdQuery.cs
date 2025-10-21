namespace Qualifier.Application.Database.RiskTreatment.Queries.GetRiskTreatmentById
{
    public interface IGetRiskTreatmentByIdQuery
    {
        Task<Object> Execute(int riskTreatmentId);
    }
}

