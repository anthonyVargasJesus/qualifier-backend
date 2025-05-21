namespace Qualifier.Application.Database.RiskTreatment.Commands.UpdateRiskTreatment
{
    public interface IUpdateRiskTreatmentCommand
    {
        Task<Object> Execute(UpdateRiskTreatmentDto model, int id);
    }
}

