namespace Qualifier.Application.Database.RequirementInDefaultRisk.Commands.UpdateRequirementInDefaultRisk
{
    public interface IUpdateRequirementInDefaultRiskCommand
    {
        Task<Object> Execute(UpdateRequirementInDefaultRiskDto model, int id);
    }
}

