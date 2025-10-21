using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
	public class RoleEntity : BaseEntity
	{
		public int roleId { get; set; }
		public string? code { get; set; }
		public string? name { get; set; }
        public int companyId { get; set; }
        [NotMapped]
        public List<MenuEntity>? menus { get; set; }
        public ICollection<RoleInUserEntity> roleInUsers { get; set; }
        [NotMapped]
        public ICollection<MenuInRoleEntity> menuInRoles { get; set; }
    }
}
