namespace Qualifier.Application.Database.OptionInMenuInRole.Commands.DeleteOptionInMenuInRole
{
    public interface IDeleteOptionInMenuInRoleCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

