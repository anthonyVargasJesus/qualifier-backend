namespace Qualifier.Application.Database.Creator.Queries.GetAllCreatorsByVersionId
{
    public interface IGetAllCreatorsByVersionIdQuery
    {
        Task<Object> Execute(int versionId);
    }
}

