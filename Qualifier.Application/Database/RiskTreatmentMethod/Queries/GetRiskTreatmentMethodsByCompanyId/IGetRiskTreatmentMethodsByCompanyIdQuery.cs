namespace Qualifier.Application.Database.RiskTreatmentMethod.Queries.GetRiskTreatmentMethodsByCompanyId
{
    public interface IGetRiskTreatmentMethodsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

