namespace Qualifier.Application.Database.RiskTreatmentMethod.Queries.GetAllRiskTreatmentMethodsByCompanyId
{
    public interface IGetAllRiskTreatmentMethodsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

