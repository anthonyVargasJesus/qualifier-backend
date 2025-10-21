namespace Qualifier.Application.Database.RiskStatus.Commands.UpdateRiskStatus
{
    public interface IUpdateRiskStatusCommand
    {
        Task<Object> Execute(UpdateRiskStatusDto model, int id);
    }
}

