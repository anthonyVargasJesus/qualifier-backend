namespace Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationById
{
    public interface IGetControlEvaluationByIdQuery
    {
        Task<Object> Execute(int controlEvaluationId);
    }
}

