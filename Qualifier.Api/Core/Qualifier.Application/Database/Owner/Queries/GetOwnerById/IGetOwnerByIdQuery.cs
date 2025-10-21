namespace Qualifier.Application.Database.Owner.Queries.GetOwnerById
{
    public interface IGetOwnerByIdQuery
    {
        Task<Object> Execute(int ownerId);
    }
}

