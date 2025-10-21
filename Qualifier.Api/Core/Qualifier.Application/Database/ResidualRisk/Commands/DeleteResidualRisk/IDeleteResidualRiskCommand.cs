namespace Qualifier.Application.Database.ResidualRisk.Commands.DeleteResidualRisk
{
    public interface IDeleteResidualRiskCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

