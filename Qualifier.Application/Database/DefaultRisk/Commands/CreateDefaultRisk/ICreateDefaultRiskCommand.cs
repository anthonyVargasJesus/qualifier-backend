namespace Qualifier.Application.Database.DefaultRisk.Commands.CreateDefaultRisk
{
    public interface ICreateDefaultRiskCommand
    {
        Task<Object> Execute(CreateDefaultRiskDto model);
    }
}

