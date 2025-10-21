namespace Qualifier.Application.Database.RiskTreatmentMethod.Commands.CreateRiskTreatmentMethod
{
    public interface ICreateRiskTreatmentMethodCommand
    {
        Task<Object> Execute(CreateRiskTreatmentMethodDto model);
    }
}

