namespace Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlsByDocumentationId
{
    public interface IGetSupportForControlsByDocumentationIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int documentationId);
    }
}

