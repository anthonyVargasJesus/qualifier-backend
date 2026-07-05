namespace Qualifier.Application.Database.UserControlGroup.Commands.SetUserControlGroups
{
    public interface ISetUserControlGroupsCommand
    {
        Task<Object> Execute(SetUserControlGroupsDto model);
    }
}
