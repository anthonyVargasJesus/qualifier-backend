using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Risk.Queries.GetRiskMonitoring
{
    public interface IGetRiskMonitoringQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search);
    }
}
