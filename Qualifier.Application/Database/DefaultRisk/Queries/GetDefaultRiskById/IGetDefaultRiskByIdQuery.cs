namespace Qualifier.Application.Database.DefaultRisk.Queries.GetDefaultRiskById
{
    public interface IGetDefaultRiskByIdQuery
    {
        Task<Object> Execute(int defaultRiskId);
    }
}

