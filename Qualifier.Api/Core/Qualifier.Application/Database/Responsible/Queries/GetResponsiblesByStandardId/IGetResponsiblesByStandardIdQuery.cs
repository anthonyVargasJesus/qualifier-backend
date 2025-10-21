namespace Qualifier.Application.Database.Responsible.Queries.GetResponsiblesByStandardId
{
    public interface IGetResponsiblesByStandardIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int standardId);
    }
}

