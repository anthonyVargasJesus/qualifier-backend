namespace Qualifier.Application.Database.Control.Queries.GetAllControlsByStandardId
{
    public interface IGetAllControlsByStandardIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

