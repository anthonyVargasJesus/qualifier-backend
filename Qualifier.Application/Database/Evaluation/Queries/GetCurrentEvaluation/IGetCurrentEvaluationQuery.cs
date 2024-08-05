using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation
{
    public interface IGetCurrentEvaluationQuery
    {
        Task<Object> Execute(int evaluationId);
    }
}
