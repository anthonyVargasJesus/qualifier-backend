namespace Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByStandardId
{
    public interface IGetControlGroupsByStandardIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int standardId);
    }
}

