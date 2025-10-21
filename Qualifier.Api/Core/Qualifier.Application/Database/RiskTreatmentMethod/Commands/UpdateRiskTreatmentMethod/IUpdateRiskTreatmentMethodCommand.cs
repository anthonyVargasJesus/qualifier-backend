namespace Qualifier.Application.Database.RiskTreatmentMethod.Commands.UpdateRiskTreatmentMethod
{
    public interface IUpdateRiskTreatmentMethodCommand
    {
        Task<Object> Execute(UpdateRiskTreatmentMethodDto model, int id);
    }
}

