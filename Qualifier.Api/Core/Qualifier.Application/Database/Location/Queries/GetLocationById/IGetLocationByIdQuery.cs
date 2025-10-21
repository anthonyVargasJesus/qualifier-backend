namespace Qualifier.Application.Database.Location.Queries.GetLocationById
{
    public interface IGetLocationByIdQuery
    {
        Task<Object> Execute(int locationId);
    }
}

