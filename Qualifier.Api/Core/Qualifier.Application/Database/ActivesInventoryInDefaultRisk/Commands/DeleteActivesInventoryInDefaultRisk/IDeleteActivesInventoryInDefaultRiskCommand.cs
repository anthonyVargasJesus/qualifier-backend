namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.DeleteActivesInventoryInDefaultRisk
{
public interface IDeleteActivesInventoryInDefaultRiskCommand
{
Task<Object> Execute(int id, int updateUserId);
}
}

