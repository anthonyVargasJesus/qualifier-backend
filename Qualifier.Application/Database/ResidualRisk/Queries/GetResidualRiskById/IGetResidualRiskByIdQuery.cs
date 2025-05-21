namespace Qualifier.Application.Database.ResidualRisk.Queries.GetResidualRiskById
{
    public interface IGetResidualRiskByIdQuery
    {
        Task<Object> Execute(int residualRiskId);
    }
}

