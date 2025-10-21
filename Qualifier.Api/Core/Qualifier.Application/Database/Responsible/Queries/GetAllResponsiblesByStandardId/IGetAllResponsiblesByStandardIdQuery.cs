namespace Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId
{
    public interface IGetAllResponsiblesByStandardIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

