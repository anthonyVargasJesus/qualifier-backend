using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Common.Application.Service
{
    public static class Pagination
    {
        public static double GetTotalPages(int totalRows, double pageSize)
        {
            return Math.Ceiling(Convert.ToDouble(totalRows) / Convert.ToDouble(pageSize));
        }
        public static Object GetPagination(int totalRows, double pageSize)
        {
            return new
            {
                totalRows = totalRows,
                totalPages = GetTotalPages(totalRows, pageSize),
            };
        }
    }
}
