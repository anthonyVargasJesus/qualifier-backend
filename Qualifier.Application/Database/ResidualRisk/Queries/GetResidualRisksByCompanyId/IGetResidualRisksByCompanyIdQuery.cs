namespace Qualifier.Application.Database.ResidualRisk.Queries.GetResidualRisksByCompanyId
{
    public interface IGetResidualRisksByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

