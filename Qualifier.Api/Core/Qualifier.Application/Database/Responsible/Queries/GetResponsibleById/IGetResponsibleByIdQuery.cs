namespace Qualifier.Application.Database.Responsible.Queries.GetResponsibleById
{
    public interface IGetResponsibleByIdQuery
    {
        Task<Object> Execute(int responsibleId);
    }
}

