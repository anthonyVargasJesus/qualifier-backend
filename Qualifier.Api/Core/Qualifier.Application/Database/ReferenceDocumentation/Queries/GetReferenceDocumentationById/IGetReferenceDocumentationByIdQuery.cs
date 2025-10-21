namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationById
{
    public interface IGetReferenceDocumentationByIdQuery
    {
        Task<Object> Execute(int referenceDocumentationId);
    }
}

