namespace Qualifier.Application.Database.MenuInRole.Commands.DeleteMenuInRole
{
public interface IDeleteMenuInRoleCommand
{
Task<Object> Execute(int id, int updateUserId);
}
}

