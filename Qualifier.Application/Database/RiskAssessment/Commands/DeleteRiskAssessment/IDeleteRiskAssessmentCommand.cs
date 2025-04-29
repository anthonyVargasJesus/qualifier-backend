namespace Qualifier.Application.Database.RiskAssessment.Commands.DeleteRiskAssessment
{
    public interface IDeleteRiskAssessmentCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

