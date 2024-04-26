namespace Qualifier.Application.Database.DefaultSection.Queries.GetAllDefaultSectionsByDocumentTypeId
{
    public interface IGetAllDefaultSectionsByDocumentTypeIdQuery
    {
        Task<Object> Execute(int documentTypeId);
    }
}

