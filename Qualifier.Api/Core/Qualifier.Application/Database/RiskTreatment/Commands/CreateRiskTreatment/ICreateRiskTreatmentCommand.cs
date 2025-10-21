namespace Qualifier.Application.Database.RiskTreatment.Commands.CreateRiskTreatment
{
    public interface ICreateRiskTreatmentCommand
    {
        Task<Object> Execute(CreateRiskTreatmentDto model);
    }
}

