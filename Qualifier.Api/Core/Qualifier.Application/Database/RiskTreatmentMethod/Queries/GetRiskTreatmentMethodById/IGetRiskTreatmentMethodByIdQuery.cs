namespace Qualifier.Application.Database.RiskTreatmentMethod.Queries.GetRiskTreatmentMethodById
{
    public interface IGetRiskTreatmentMethodByIdQuery
    {
        Task<Object> Execute(int riskTreatmentMethodId);
    }
}

