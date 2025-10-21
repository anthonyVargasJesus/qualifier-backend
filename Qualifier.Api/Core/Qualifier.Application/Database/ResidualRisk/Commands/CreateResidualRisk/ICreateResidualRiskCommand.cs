namespace Qualifier.Application.Database.ResidualRisk.Commands.CreateResidualRisk
{
    public interface ICreateResidualRiskCommand
    {
        Task<Object> Execute(CreateResidualRiskDto model);
    }
}

