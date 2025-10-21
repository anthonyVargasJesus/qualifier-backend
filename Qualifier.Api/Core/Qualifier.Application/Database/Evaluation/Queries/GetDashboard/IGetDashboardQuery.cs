using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Evaluation.Queries.GetDashboard
{
    public interface IGetDashboardQuery
    {
        Task<Object> Execute(int standardId, int evaluationId, int companyId);
    }
}
