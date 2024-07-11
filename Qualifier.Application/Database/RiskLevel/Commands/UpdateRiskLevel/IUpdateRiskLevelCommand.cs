namespace Qualifier.Application.Database.RiskLevel.Commands.UpdateRiskLevel
{
    public interface IUpdateRiskLevelCommand
    {
        Task<Object> Execute(UpdateRiskLevelDto model, int id);
    }
}

