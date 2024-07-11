using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class MenuEntity : BaseEntity
	{
		public int menuId { get; set; }
		public string? name { get; set; }
		public string? image { get; set; }
        [NotMapped]
        public int order { get; set; }
        public int companyId { get; set; }

        [NotMapped]
        public List<OptionEntity>? options { get; set; }
        [NotMapped]
        public ICollection<MenuInRoleEntity> menuInRoles { get; set; }
    }
}
