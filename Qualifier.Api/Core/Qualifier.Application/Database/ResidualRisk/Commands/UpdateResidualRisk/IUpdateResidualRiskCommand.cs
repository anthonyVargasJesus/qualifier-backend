namespace Qualifier.Application.Database.ResidualRisk.Commands.UpdateResidualRisk
{
    public interface IUpdateResidualRiskCommand
    {
        Task<Object> Execute(UpdateResidualRiskDto model, int id);
    }
}

