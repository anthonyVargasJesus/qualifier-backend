namespace Qualifier.Application.Database.Menace.Queries.GetMenaceById
{
    public interface IGetMenaceByIdQuery
    {
        Task<Object> Execute(int menaceId);
    }
}

