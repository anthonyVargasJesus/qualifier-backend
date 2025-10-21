namespace Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementsByDocumentationId
{
    public interface IGetSupportForRequirementsByDocumentationIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int documentationId);
    }
}

