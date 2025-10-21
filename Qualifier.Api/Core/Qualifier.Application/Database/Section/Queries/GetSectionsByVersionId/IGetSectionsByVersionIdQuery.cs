namespace Qualifier.Application.Database.Section.Queries.GetSectionsByVersionId
{
    public interface IGetSectionsByVersionIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int versionId);
    }
}

