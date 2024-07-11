namespace Qualifier.Application.Database.Risk.Queries.GetRiskById
{
    public interface IGetRiskByIdQuery
    {
        Task<Object> Execute(int riskId);
    }
}

