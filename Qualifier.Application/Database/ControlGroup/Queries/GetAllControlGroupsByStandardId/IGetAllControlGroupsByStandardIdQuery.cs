namespace Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByStandardId
{
    public interface IGetAllControlGroupsByStandardIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

