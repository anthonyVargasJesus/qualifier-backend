namespace Qualifier.Application.Database.Personal.Queries.GetPersonalById
{
    public interface IGetPersonalByIdQuery
    {
        Task<Object> Execute(int personalId);
    }
}

