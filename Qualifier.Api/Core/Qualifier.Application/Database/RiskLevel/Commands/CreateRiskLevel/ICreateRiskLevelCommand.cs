namespace Qualifier.Application.Database.RiskLevel.Commands.CreateRiskLevel
{
    public interface ICreateRiskLevelCommand
    {
        Task<Object> Execute(CreateRiskLevelDto model);
    }
}

