

namespace Qualifier.Application.Database.Evaluation.Queries.GetPendingDocumentation
{
    public interface IGetPendingDocumentationQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int standardId, int evaluationId);
    }
}
