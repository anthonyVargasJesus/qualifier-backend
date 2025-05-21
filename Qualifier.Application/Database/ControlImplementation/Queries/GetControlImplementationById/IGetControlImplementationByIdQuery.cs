namespace Qualifier.Application.Database.ControlImplementation.Queries.GetControlImplementationById
{
    public interface IGetControlImplementationByIdQuery
    {
        Task<Object> Execute(int controlImplementationId);
    }
}

