using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class RoleInUserEntity : BaseEntity
    {
        public int roleInUserId { get; set; }
        public RoleEntity role { get; set; }
        public UserEntity user { get; set; }
        public int roleId { get; set; }
        public int userId { get; set; }
    }
}
