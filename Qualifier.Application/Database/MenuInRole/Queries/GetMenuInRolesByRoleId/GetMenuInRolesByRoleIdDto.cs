namespace Qualifier.Application.Database.MenuInRole.Queries.GetMenuInRolesByRoleId
{
    public class GetMenuInRolesByRoleIdDto
    {
        public int menuInRoleId { get; set; }
        public int menuId { get; set; }
        public int order { get; set; }
        public GetMenuInRolesByRoleIdMenuDto? menu { get; set; }
    }
    public class GetMenuInRolesByRoleIdMenuDto
    {
        public string name { get; set; }

    }
}


