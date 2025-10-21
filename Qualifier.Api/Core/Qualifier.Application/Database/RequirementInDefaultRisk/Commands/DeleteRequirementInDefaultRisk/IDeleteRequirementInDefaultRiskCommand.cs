namespace Qualifier.Application.Database.RequirementInDefaultRisk.Commands.DeleteRequirementInDefaultRisk
{
    public interface IDeleteRequirementInDefaultRiskCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

