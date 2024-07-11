namespace Qualifier.Application.Database.Risk.Commands.UpdateRisk
{
    public interface IUpdateRiskCommand
    {
        Task<Object> Execute(UpdateRiskDto model, int id);
    }
}

