namespace Qualifier.Application.Database.Creator.Queries.GetCreatorById
{
    public interface IGetCreatorByIdQuery
    {
        Task<Object> Execute(int creatorId);
    }
}

