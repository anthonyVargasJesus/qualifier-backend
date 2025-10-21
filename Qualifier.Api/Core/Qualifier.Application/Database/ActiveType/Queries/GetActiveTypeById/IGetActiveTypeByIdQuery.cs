namespace Qualifier.Application.Database.ActiveType.Queries.GetActiveTypeById
{
    public interface IGetActiveTypeByIdQuery
    {
        Task<Object> Execute(int activeTypeId);
    }
}

