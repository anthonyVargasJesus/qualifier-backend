namespace Qualifier.Application.Database.RiskAssessment.Queries.GetRiskAssessmentById
{
    public interface IGetRiskAssessmentByIdQuery
    {
        Task<Object> Execute(int riskAssessmentId);
    }
}

