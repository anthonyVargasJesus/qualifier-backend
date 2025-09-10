namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByControlEvaluationId
{
    public interface IGetReferenceDocumentationsByControlEvaluationIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int controlEvaluationId);
    }
}

