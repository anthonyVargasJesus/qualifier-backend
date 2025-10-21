namespace Qualifier.Application.Database.DefaultRisk.Commands.UpdateDefaultRisk
{
    public interface IUpdateDefaultRiskCommand
    {
        Task<Object> Execute(UpdateDefaultRiskDto model, int id);
    }
}

