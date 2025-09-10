namespace Qualifier.Application.Database.ControlInDefaultRisk.Commands.DeleteControlInDefaultRisk
{
    public interface IDeleteControlInDefaultRiskCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

