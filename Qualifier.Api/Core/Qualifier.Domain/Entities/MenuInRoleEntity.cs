using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class MenuInRoleEntity : BaseEntity
    {
        public int menuInRoleId { get; set; }
        public MenuEntity menu { get; set; }
        public RoleEntity role { get; set; }
        public int order { get; set; }
        public int menuId { get; set; }
        public int roleId { get; set; }
        public int companyId { get; set; }
    }
}
