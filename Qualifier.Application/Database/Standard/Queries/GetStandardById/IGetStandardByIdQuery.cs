namespace Qualifier.Application.Database.Standard.Queries.GetStandardById
{
    public interface IGetStandardByIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

