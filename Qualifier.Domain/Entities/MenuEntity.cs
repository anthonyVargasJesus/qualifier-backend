using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class MenuEntity : BaseEntity
	{
		public int menuId { get; set; }
		public string? name { get; set; }
		public string? image { get; set; }
        public int order { get; set; }
        public List<OptionEntity>? options { get; set; }
        public ICollection<MenuInRoleEntity> menuInRoles { get; set; }
    }
}
