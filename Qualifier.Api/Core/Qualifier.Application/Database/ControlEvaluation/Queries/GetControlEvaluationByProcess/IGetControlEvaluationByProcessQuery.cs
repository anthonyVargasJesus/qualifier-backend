using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess
{
    public interface IGetControlEvaluationByProcessQuery
    {
        Task<Object> Execute(int standardId, int evaluationId);
    }
}
