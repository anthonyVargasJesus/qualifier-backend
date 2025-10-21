namespace Qualifier.Application.Database.RiskLevel.Commands.DeleteRiskLevel
{
    public interface IDeleteRiskLevelCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

