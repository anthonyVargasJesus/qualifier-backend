using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public int userId { get; set; }
        public string? name { get; set; }
        public string? middleName { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public int userStateId { get; set; }
        public string? image { get; set; }
        public string? userName { get; set; }
        public UserStateEntity userState { get; set; }
        public ICollection<RoleInUserEntity> roleInUsers { get; set; }
        public int companyId { get; set; }

    }
}

