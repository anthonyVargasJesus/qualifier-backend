namespace Qualifier.Application.Database.Section.Queries.GetAllSectionsByVersionId
{
    public interface IGetAllSectionsByVersionIdQuery
    {
        Task<Object> Execute(int versionId);
    }
}

