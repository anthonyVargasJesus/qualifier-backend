namespace Qualifier.Application.Database.Version.Queries.GetVersionById
{
    public interface IGetVersionByIdQuery
    {
        Task<Object> Execute(int versionId);
    }
}

