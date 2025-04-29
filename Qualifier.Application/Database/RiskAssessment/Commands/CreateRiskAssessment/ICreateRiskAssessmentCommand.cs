namespace Qualifier.Application.Database.RiskAssessment.Commands.CreateRiskAssessment
{
    public interface ICreateRiskAssessmentCommand
    {
        Task<Object> Execute(CreateRiskAssessmentDto model);
    }
}

