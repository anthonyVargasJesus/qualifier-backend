namespace Qualifier.Application.Database.MenuInRole.Queries.GetAllMenuInRolesByRoleId
{
    public class GetAllMenuInRolesByRoleIdDto
    {
        public int menuInRoleId { get; set; }
        public int menuId { get; set; }
        public int order { get; set; }
        public GetAllMenuInRolesByRoleIdMenuDto? menu { get; set; }
    }
    public class GetAllMenuInRolesByRoleIdMenuDto
    {
        public string name { get; set; }

    }
}

