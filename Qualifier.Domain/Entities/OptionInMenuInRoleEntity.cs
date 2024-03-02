using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class OptionInMenuInRoleEntity : BaseEntity
    {
        public int optionInMenuInRoleId { get; set; }
        public MenuEntity menu { get; set; }
        public OptionEntity option { get; set; }
        public RoleEntity role { get; set; }
        public int order { get; set; }
        public int optionId { get; set; }
        public int menuId { get; set; }
        public int roleId { get; set; }
    }
}
