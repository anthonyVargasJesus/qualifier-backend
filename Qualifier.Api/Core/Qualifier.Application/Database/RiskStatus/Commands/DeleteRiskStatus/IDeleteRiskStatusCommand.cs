namespace Qualifier.Application.Database.RiskStatus.Commands.DeleteRiskStatus
{
    public interface IDeleteRiskStatusCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

