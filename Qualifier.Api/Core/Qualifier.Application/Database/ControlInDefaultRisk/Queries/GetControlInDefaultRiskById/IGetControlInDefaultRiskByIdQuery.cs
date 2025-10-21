namespace Qualifier.Application.Database.ControlInDefaultRisk.Queries.GetControlInDefaultRiskById
{
    public interface IGetControlInDefaultRiskByIdQuery
    {
        Task<Object> Execute(int controlInDefaultRiskId);
    }
}

