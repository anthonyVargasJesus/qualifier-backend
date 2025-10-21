namespace Qualifier.Application.Database.RiskAssessment.Commands.UpdateRiskAssessment
{
    public interface IUpdateRiskAssessmentCommand
    {
        Task<Object> Execute(UpdateRiskAssessmentDto model, int id);
    }
}

