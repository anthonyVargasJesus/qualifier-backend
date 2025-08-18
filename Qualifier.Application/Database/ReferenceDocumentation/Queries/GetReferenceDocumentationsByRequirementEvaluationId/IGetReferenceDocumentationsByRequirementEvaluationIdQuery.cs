namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByRequirementEvaluationId
{
    public interface IGetReferenceDocumentationsByRequirementEvaluationIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int requirementEvaluationId);
    }
}

