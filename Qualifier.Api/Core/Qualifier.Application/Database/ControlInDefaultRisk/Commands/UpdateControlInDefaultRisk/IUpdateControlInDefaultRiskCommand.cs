namespace Qualifier.Application.Database.ControlInDefaultRisk.Commands.UpdateControlInDefaultRisk
{
    public interface IUpdateControlInDefaultRiskCommand
    {
        Task<Object> Execute(UpdateControlInDefaultRiskDto model, int id);
    }
}

