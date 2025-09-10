namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.CreateActivesInventoryInDefaultRisk
{
public interface ICreateActivesInventoryInDefaultRiskCommand
{
Task<Object> Execute(CreateActivesInventoryInDefaultRiskDto model);
}
}

