namespace Qualifier.Application.Database.ControlInDefaultRisk.Commands.CreateControlInDefaultRisk
{
    public interface ICreateControlInDefaultRiskCommand
    {
        Task<Object> Execute(CreateControlInDefaultRiskDto model);
    }
}

