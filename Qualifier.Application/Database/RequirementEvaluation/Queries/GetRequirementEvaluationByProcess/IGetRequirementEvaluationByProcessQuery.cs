using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess
{
    public interface IGetRequirementEvaluationByProcessQuery
    {
        Task<Object> Execute(int standardId, int evaluationId);
    }
}
