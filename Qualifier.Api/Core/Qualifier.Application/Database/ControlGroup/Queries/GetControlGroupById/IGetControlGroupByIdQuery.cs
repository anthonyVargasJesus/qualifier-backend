namespace Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupById
{
    public interface IGetControlGroupByIdQuery
    {
        Task<Object> Execute(int controlGroupId);
    }
}

