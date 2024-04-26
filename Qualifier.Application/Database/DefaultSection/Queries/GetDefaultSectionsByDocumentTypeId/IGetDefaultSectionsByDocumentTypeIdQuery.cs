namespace Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionsByDocumentTypeId
{
    public interface IGetDefaultSectionsByDocumentTypeIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int documentTypeId);
    }
}

