using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class UserStateEntity : BaseEntity
    {
        public int userStateId { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public int companyId { get; set; }
        public ICollection<UserEntity> users { get; set; }
    }
}

