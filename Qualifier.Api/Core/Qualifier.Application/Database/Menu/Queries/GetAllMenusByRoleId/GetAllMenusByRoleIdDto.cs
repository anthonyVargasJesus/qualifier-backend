using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Menu.Queries.GetAllMenusByRoleId
{
    public class GetAllMenusByRoleIdDto
    {
        public int menuId { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public List<GetAllMenusOptionByRoleIdDto> options { get; set; }
    }

    public class GetAllMenusOptionByRoleIdDto
    {
        public int optionId { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public bool isMobile { get; set; }
        public bool isChecked { get; set; }
        public int order { get; set; }
        public int optionInMenuInRoleId { get; set; }
    }

}
