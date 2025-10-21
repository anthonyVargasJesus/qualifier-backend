namespace Qualifier.Application.Database.Control.Queries.GetControlsByControlGroupId
{
    public interface IGetControlsByControlGroupIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int controlGroupId);
    }
}

