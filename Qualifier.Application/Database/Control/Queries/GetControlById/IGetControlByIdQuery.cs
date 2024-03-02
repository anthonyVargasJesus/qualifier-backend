namespace Qualifier.Application.Database.Control.Queries.GetControlById
{
    public interface IGetControlByIdQuery
    {
        Task<Object> Execute(int controlId);
    }
}

