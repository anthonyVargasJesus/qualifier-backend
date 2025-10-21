namespace Qualifier.Application.Database.Risk.Commands.DeleteRisk
{
    public interface IDeleteRiskCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

