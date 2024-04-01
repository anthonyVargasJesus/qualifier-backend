namespace Qualifier.Application.Database.Evaluation.Queries.GetEvaluationById
{
    public interface IGetEvaluationByIdQuery
    {
        Task<Object> Execute(int evaluationId);
    }
}

