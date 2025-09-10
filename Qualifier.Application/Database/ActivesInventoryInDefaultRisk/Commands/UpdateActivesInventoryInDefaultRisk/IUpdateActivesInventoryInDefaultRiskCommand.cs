namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.UpdateActivesInventoryInDefaultRisk
{
public interface IUpdateActivesInventoryInDefaultRiskCommand
{
Task<Object> Execute(UpdateActivesInventoryInDefaultRiskDto model, int id);
}
}

