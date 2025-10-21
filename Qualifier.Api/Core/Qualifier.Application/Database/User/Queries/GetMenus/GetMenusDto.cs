using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.User.Queries.GetMenus
{
    public class GetMenusDto
    {
    }

    public class GetMenusMenuQueryDto
    {
        public int menuId { get; set; }
        public string? name { get; set; }
        public string? image { get; set; }
        public int order { get; set; }
        public List<GetMenusOptionQueryDto>? options { get; set; }
    }

    public class GetMenusOptionQueryDto
    {
        public int optionId { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public bool isMobile { get; set; }
        public bool isChecked { get; set; }
        public int optionInMenuInRoleId { get; set; }
    }

}
