namespace Qualifier.Application.Database.RiskStatus.Commands.CreateRiskStatus
{
    public interface ICreateRiskStatusCommand
    {
        Task<Object> Execute(CreateRiskStatusDto model);
    }
}

