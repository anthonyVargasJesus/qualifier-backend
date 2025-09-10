namespace Qualifier.Application.Database.RequirementInDefaultRisk.Commands.CreateRequirementInDefaultRisk
{
    public interface ICreateRequirementInDefaultRiskCommand
    {
        Task<Object> Execute(CreateRequirementInDefaultRiskDto model);
    }
}

