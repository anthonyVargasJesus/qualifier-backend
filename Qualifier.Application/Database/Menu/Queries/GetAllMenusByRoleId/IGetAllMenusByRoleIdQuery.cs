using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Menu.Queries.GetAllMenusByRoleId
{
    public interface IGetAllMenusByRoleIdQuery
    {
        Task<Object> Execute(int roleId, int companyId);
    }
}
