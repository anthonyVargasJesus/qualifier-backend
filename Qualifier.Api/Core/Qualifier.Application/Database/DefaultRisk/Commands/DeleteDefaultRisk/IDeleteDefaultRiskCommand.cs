namespace Qualifier.Application.Database.DefaultRisk.Commands.DeleteDefaultRisk
{
    public interface IDeleteDefaultRiskCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

