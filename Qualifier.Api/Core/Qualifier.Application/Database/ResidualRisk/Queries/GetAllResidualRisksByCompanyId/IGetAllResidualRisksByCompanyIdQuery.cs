namespace Qualifier.Application.Database.ResidualRisk.Queries.GetAllResidualRisksByCompanyId
{
    public interface IGetAllResidualRisksByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

